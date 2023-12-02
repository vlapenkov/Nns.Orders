using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Logic
{
    public interface IWorkOrderService
    {
         Task<long> Add(CreateWorkOrderRequest request);

        Task<WorkOrderResponse> Get(long id);
    }
}