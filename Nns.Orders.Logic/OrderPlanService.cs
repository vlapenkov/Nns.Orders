using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Logic
{
    public class OrderPlanService : IOrderPlanService
    {
        private readonly IOrderDbContext _dbContext;

        public OrderPlanService(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Add(CreateOrderPlanRequest request)
        {

            if (await _dbContext.OrderPlan.AnyAsync(p => p.SettlementId == p.SettlementId && p.StartDate > request.StartDate))
            {
                throw new AppException("Нельзя вводить данные задним числом. Уже есть более поздние записи.");
            }

            // проверка что нет таких на StartDate
            var workingPlans = await _dbContext.OrderPlan.Where(p =>
               p.SettlementId == p.SettlementId
               && p.StartDate == request.StartDate)
                .Select(p => new { p.WorkKindId, p.OrderNumber }).AsNoTracking().ToListAsync();

            if (workingPlans.Any(self => self.WorkKindId == request.WorkKindId))
            {
                throw new ApplicationException($"На дату {request.StartDate} уже есть WorkKindId='{request.WorkKindId}'");
            }

            if (!await CanMachineApply(request.SettlementId, request.WorkKindId, request.MachineKindId))
            {
                throw new ApplicationException($"Применяемость не соответствует для выработки {request.SettlementId} {request.WorkKindId} {request.MachineKindId}");
            }

            List<WorkOrder> workOrders = await _dbContext.WorkOrders.Where(p => p.SettlementId == request.SettlementId).AsNoTracking().ToListAsync();

            WorkOrder[] activeWorkOrders = workOrders.GroupBy(x => x.WorkKindId, (key, g) => g.OrderByDescending(e => e.StartDate).First())
                .Where(p => p.IsActive).OrderBy(p => p.OrderNumber).ToArray();


            workingPlans.Union(new[] { new { request.WorkKindId, request.OrderNumber } }).OrderBy(p => p.OrderNumber);

            int savedIndex = -1;
            foreach (var wPlan in workingPlans)
            {
                int foundIndex = Array.FindIndex(activeWorkOrders, p => p.WorkKindId == wPlan.WorkKindId);
                if (foundIndex < 0)
                {
                    throw new AppException("Данного вида работ нет в производственном цикле");
                }

                if (savedIndex > foundIndex)
                {
                    throw new AppException("Порядок видов работ в плане не соответствует порядку в активном производственном цикле");
                }

                savedIndex = foundIndex;
            }

            OrderPlan plan = new()
            {
                StartDate = request.StartDate,
                SettlementId = request.SettlementId,
                WorkKindId = request.WorkKindId,
                MachineKindId = request.MachineKindId,
                Value = request.Value,
                OrderNumber = request.OrderNumber,
                IsComplete = request.IsComplete
            };

            _dbContext.OrderPlan.Add(plan);

            return await _dbContext.SaveChangesAsync();

        }

        public async Task<bool> CanMachineApply(long settlementId, long workKindId, long machineKindId)
        {

            MachineApplication? lastRecord = await _dbContext.MachineApplications.Where(p => p.SettlementId == settlementId && p.WorkKindId == workKindId && p.MachineKindId == machineKindId)
                  .OrderByDescending(p => p.StartDate).FirstOrDefaultAsync();

            return lastRecord != null && lastRecord.IsActive;

        }

        public async Task<OrderPlanResponse> Get(long id)
        {
            var result = await _dbContext.OrderPlan
                .Include(p => p.WorkKind)
                .Include(p => p.MachineKind)
                .Include(p => p.Settlement)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException($"Сущность не найдена по ключу {id}");

            return new OrderPlanResponse
            {
                Id = result.Id,
                IsComplete = result.IsComplete,
                MachineKind = new MachineKindDto { Id = result.MachineKindId, Name = result.MachineKind.Name },
                WorkKind = new WorkKindDto { Id = result.WorkKindId, Name = result.WorkKind.Name },
                Settlement = new SettlementDto { Id = result.SettlementId, Name = result.Settlement.Name },
                OrderNumber = result.OrderNumber,
                Value = result.Value
            };
        }

        public async Task<PagedList<OrderPlanResponse>> Get(OrderPlanFilter filter)
        {
            IQueryable<OrderPlan> query = _dbContext.OrderPlan.AsNoTracking();

            if (filter.SettlementId != null)
            {
                query = query.Where(x => x.SettlementId== filter.SettlementId);
            }

            if (filter.WorkKindId != null)
            {
                query = query.Where(x => x.WorkKindId == filter.WorkKindId);
            }
            
            long count = await query.CountAsync();

            query = query.AsNoTracking().OrderByDescending(x => x.Created);

            query = SetPagination(query!, filter);

            PagedList<OrderPlanResponse> result = new()
            {
                Items = await query.Select(result => new OrderPlanResponse
                {
                    Id = result.Id,
                    IsComplete = result.IsComplete,
                    MachineKind = new MachineKindDto { Id = result.MachineKindId, Name = result.MachineKind.Name },
                    WorkKind = new WorkKindDto { Id = result.WorkKindId, Name = result.WorkKind.Name },
                    Settlement = new SettlementDto { Id = result.SettlementId, Name = result.Settlement.Name },
                    OrderNumber = result.OrderNumber,
                    Value = result.Value
                }).ToListAsync(),
                PageNumber = filter.PageNumber!.Value,
                PageSize = filter.PageSize,
            };

            return result;

        }

        private IQueryable<OrderPlan> SetPagination(IQueryable<OrderPlan> query, OrderPlanFilter filter)
        {
            if (!filter.PageSize.HasValue || filter.PageSize == default)
            {
                filter.PageSize = 50;
            }
            filter.PageNumber = Math.Max(1, filter.PageNumber.GetValueOrDefault());

            int? position = (filter.PageNumber - 1) * filter.PageSize;

            query = filter.PageSize.Value == int.MaxValue ?
                query
                .Skip(position ?? 0)
                : query
                .Skip(position ?? 0)
                .Take(filter.PageSize.Value);

            return query;
        }
    }
}
