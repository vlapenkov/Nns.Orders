using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;

namespace Nns.Orders.Logic;

public class WorkCycleService : IWorkCycleService
{
    private readonly IOrderDbContext _dbContext;
    private readonly IWorkCycleState _stateService;

    public WorkCycleService(IOrderDbContext dbContext, IWorkCycleState stateService)
    {
        _dbContext = dbContext;
        _stateService = stateService;
    }

    public async Task<long> Add(WorkCycle model)
    {
        await CheckIfRetro(model);

        await CheckExistanceOnDate(model);

        await CheckOrdersLater(model);

        await CheckActiveRecords(model);

        _dbContext.WorkCycles.Add(model);

        await _dbContext.SaveChangesAsync();

        return model.Id;
    }

    public async Task<WorkCycle> Get(long id)
    {
        var result = await _dbContext.WorkCycles
                         .Include(p => p.WorkType)
                         .FirstOrDefaultAsync(p => p.Id == id)
                     ?? throw new EntityNotFoundException($"Сущность не найдена по ключу {id}");

        return result;
    }

    private async Task CheckActiveRecords(WorkCycle model)
    {
        var activeRecords = await _stateService.GetWorkCycles(model.StartDate, _ => true);

        if (model.IsActive)
        {
            if (activeRecords.Any(self => self.WorkTypeId == model.WorkTypeId && self.IsActive))
                throw new AppException($"Уже есть активная запись на WorkTypeId='{model.WorkTypeId}'");

            if (activeRecords.Any(self => self.OrderNumber == model.OrderNumber && self.IsActive))
                throw new AppException($"Уже есть активная запись на  OrderNumber='{model.OrderNumber}' ");
        }
        else
        {
            if (!activeRecords.Any(self => self.WorkTypeId == model.WorkTypeId
                                           && self.OrderNumber == model.OrderNumber
                                           && self.IsActive
                                           && self.StartDate < model.StartDate))
                throw new AppException("Не нашел запись для отмены");
        }
    }

    private async Task CheckExistanceOnDate(WorkCycle model)
    {
        if (await _dbContext.WorkCycles.AnyAsync(
                p => p.StartDate == model.StartDate && p.WorkTypeId == model.WorkTypeId))
            throw new AppException($"Вид работы за данную дату уже установлен: {model.WorkTypeId}");

        if (await _dbContext.WorkCycles.AnyAsync(p =>
                p.StartDate == model.StartDate && p.OrderNumber == model.OrderNumber))
        throw new AppException($"Такой порядок работы уже установлен: {model.OrderNumber}");
    }


    private async Task CheckOrdersLater(WorkCycle model)
    {
        if (await _dbContext.WorkOrders.AnyAsync(p => p.StartDate.Date >= model.StartDate))
            throw new AppException("Есть задания позднее текущего применения.");
    }

    private async Task CheckIfRetro(WorkCycle model)
    {
        if (await _dbContext.WorkCycles.AnyAsync(p => p.StartDate > model.StartDate))
            throw new AppException("Нельзя вводить данные задним числом. Уже есть более поздние записи.");
    }
}