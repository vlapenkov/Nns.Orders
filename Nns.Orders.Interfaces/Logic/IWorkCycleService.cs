using Nns.Orders.Domain.Documents;


namespace Nns.Orders.Interfaces.Logic
{
    public interface IWorkCycleService
    {
        Task<long> Add(WorkCycle model);

        Task<WorkCycle> Get(long id);
    }
}