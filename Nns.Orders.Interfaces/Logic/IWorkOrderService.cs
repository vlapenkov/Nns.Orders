using Nns.Orders.Common.PagedList;
using Nns.Orders.Domain.Documents;


namespace Nns.Orders.Interfaces.Logic;

public interface IWorkOrderService
{
    Task<long> Add(WorkOrder request);
    Task<WorkOrder> Get(long Id);

    //Task<PagedList<WorkOrder>> Get(WorkOrderFilter filter);
}