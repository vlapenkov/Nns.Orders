
using Nns.Orders.Domain.Documents;


namespace Nns.Orders.Interfaces.Logic;

public interface IWorkOrderService
{
    Task<long> Add(WorkOrder request);
    Task<WorkOrder> Get(long id);

    IQueryable<WorkOrder> GetAll(long? excavationId, long? workTypeId);
    
}