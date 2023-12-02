using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Logic
{
    public class WorkOrderService : IWorkOrderService
    {
        private readonly IOrderDbContext _dbContext;

        public WorkOrderService(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Add(CreateWorkOrderRequest request)
        {
            if (await _dbContext.WorkOrders.AnyAsync(p => p.SettlementId == p.SettlementId && p.StartDate > request.StartDate))
                throw new AppException("Нельзя вводить данные задним числом. Уже есть более поздние записи.");


            bool hasApplication = await _dbContext.WorkOrders.AsNoTracking().AnyAsync(p =>
              p.StartDate == request.StartDate
              && p.SettlementId == request.SettlementId
              && p.WorkKindId == request.WorkKindId
              );

            if (hasApplication)
            {
                throw new AppException("Вид работы за данную дату уже установлен");
            }


            List<WorkOrder> records = await _dbContext.WorkOrders.AsNoTracking().Where(p => p.SettlementId == request.SettlementId).ToListAsync();

            WorkOrder[] activeRecords = records.GroupBy(x => x.WorkKindId, (key, g) => g.OrderByDescending(e => e.StartDate).First())
                .Where(p => p.IsActive).ToArray();


            if (activeRecords.Any(self => self.WorkKindId == request.WorkKindId && self.OrderNumber == request.OrderNumber))
            {
                throw new ApplicationException($"Уже есть активная запись на WorkKindId='{request.WorkKindId}' и OrderNumber='{request.OrderNumber}' ");
            }

            var model = new WorkOrder
            {
                StartDate = request.StartDate,                
                SettlementId = request.SettlementId,
                WorkKindId = request.WorkKindId,
                IsActive = request.IsActive
            };

            _dbContext.WorkOrders.Add(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        public async Task<WorkOrderResponse> Get(long id)
        {
            WorkOrder result = await _dbContext.WorkOrders
                .Include(p => p.Settlement)
                .Include(p => p.WorkKind)                
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException($"Сущность не найдена по ключу {id}");

            return new WorkOrderResponse
            {
                Id = result.Id,                
                Settlement = new SettlementDto { Id = result.SettlementId, Name = result.Settlement.Name },
                WorkKind = new WorkKindDto { Id = result.WorkKind.Id, Name = result.WorkKind.Name },
                IsActive = result.IsActive,
                OrderNumber = result.OrderNumber,
                StartDate= result.StartDate,    
            };


        }
    }
}