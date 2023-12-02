using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Interfaces.Logic
{
    public interface IOrderPlanService
    {
        Task<long> Add(CreateOrderPlanRequest request);
        Task<OrderPlanResponse> Get(long Id);
    }
}