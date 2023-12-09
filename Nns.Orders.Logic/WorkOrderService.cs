using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;

namespace Nns.Orders.Logic;

public class WorkOrderService : IWorkOrderService
{
    private record struct WorkWithOrderNumber(long WorkTypeId, uint OrderNumber);

    private readonly IOrderDbContext _dbContext;
    private readonly IWorkCycleState _workCyclesStateService;
    private readonly IEquipmentState _equipmentState;

    public WorkOrderService(IOrderDbContext dbContext, IWorkCycleState workCyclesStateService, IEquipmentState equipmentState)
    {
        _dbContext = dbContext;
        _workCyclesStateService = workCyclesStateService;
        _equipmentState = equipmentState;
    }

    public async Task<long> Add(WorkOrder model)
    {
        await CheckIfRetro(model);

        await CheckExistanceOnDate(model);
        
        var workingPlansOnDate = await GetWorkOrdersOnDate(model);

        await CheckApplicationWorkTypeAndEquipment(model);

        var activeWorkCycles = await _workCyclesStateService.GetWorkCycles(model.StartDate, ws => ws.IsActive);

        CheckOrderNumbers(model, workingPlansOnDate, activeWorkCycles);

        _dbContext.WorkOrders.Add(model);

        await _dbContext.SaveChangesAsync();

        return model.Id;
    }

    /// <summary>
    ///     Может ли быть применен для выработки вид машины и вид работы
    /// </summary>
    public async Task<WorkOrder> Get(long id)
    {
        var result = await _dbContext.WorkOrders
                         .Include(p => p.WorkType)
                         .Include(p => p.EquipmentType)
                         .Include(p => p.Excavation)
                         .FirstOrDefaultAsync(p => p.Id == id)
                     ?? throw new EntityNotFoundException($"Сущность не найдена по ключу {id}");

        return result;
    }

    private async Task CheckApplicationWorkTypeAndEquipment(WorkOrder model)
    {
        var application =await _equipmentState.GetApplication(model.StartDate, model.WorkTypeId, model.EquipmentTypeId);

        if(application==null || !application.IsActive)
            throw new AppException(
                $"Применяемость не активна WorkTypeId={model.WorkTypeId} EquipmentTypeId={model.EquipmentTypeId}");
    }

    private async Task CheckExistanceOnDate(WorkOrder model)
    {
        if (await _dbContext.WorkOrders.AnyAsync(p =>
                p.StartDate == model.StartDate && p.ExcavationId == model.ExcavationId &&
                p.WorkTypeId == model.WorkTypeId))
            throw new AppException($"Вид работы за данную дату уже установлен: {model.WorkTypeId}");

        if (await _dbContext.WorkOrders.AnyAsync(p =>
                p.StartDate == model.StartDate && p.ExcavationId == model.ExcavationId &&
                p.OrderNumber == model.OrderNumber)) 
        throw new AppException($"Такой порядок работы уже установлен: {model.OrderNumber}");
    }

    private static void CheckOrderNumbers(WorkOrder model, WorkWithOrderNumber[] workingPlansOnDate,
        WorkCycle[] activeWorkCycles)
    {
        var workingPlansWithCurrent = workingPlansOnDate
            .Concat(new[] {new WorkWithOrderNumber(model.WorkTypeId, model.OrderNumber)}).OrderBy(p => p.OrderNumber);

        var savedIndex = -1;
        foreach (var wPlan in workingPlansWithCurrent)
        {
            var foundIndex = Array.FindIndex(activeWorkCycles, p => p.WorkTypeId == wPlan.WorkTypeId);
            if (foundIndex < 0) throw new AppException("Данного вида работ нет в производственном цикле");

            if (savedIndex > foundIndex)
                throw new AppException(
                    "Порядок видов работ в плане не соответствует порядку в активном производственном цикле");

            savedIndex = foundIndex;
        }
    }

    private async Task<WorkWithOrderNumber[]> GetWorkOrdersOnDate(WorkOrder model)
    {
        var workingPlansOnDate = await _dbContext.WorkOrders.Where(p =>
                p.ExcavationId == model.ExcavationId
                && p.StartDate == model.StartDate)
            .Select(p => new WorkWithOrderNumber(p.WorkTypeId, p.OrderNumber))
            .ToArrayAsync();       

        return workingPlansOnDate;
    }


    private async Task CheckIfRetro(WorkOrder model)
    {
        if (await _dbContext.WorkOrders.AnyAsync(p =>
                p.ExcavationId == model.ExcavationId && p.StartDate > model.StartDate))
            throw new AppException("Нельзя вводить данные задним числом. В заданиях уже есть более поздние записи.");

        //if (await _dbContext.WorkCycles.AnyAsync(p => p.StartDate > model.StartDate))
        //    throw new AppException(
        //        "Нельзя вводить данные задним числом. В произв. циклах уже есть более поздние записи.");

        //if (await _dbContext.EquipmentApplications.AnyAsync(p => p.StartDate > model.StartDate))
        //    throw new AppException(
        //        "Нельзя вводить данные задним числом. В применяемости уже есть более поздние записи.");
    }



    public  IQueryable<WorkOrder> GetAll(long? excavationId, long? workTypeId)
    {

        var query = _dbContext.WorkOrders
            .Include(self=>self.Excavation)                        
            .AsNoTracking();

        if (excavationId != null)
        {
            query = query.Where(x => x.ExcavationId == excavationId);
        }

        if (workTypeId != null)
        {
            query = query.Where(x => x.WorkTypeId == workTypeId);
        }

        return query.OrderByDescending(self => self.Created);
                 
    }


}