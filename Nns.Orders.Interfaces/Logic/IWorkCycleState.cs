using Nns.Orders.Domain.Documents;
using System.Linq.Expressions;

namespace Nns.Orders.Interfaces.Logic
{
    public interface IWorkCycleState
    {
        Task<WorkCycle[]> GetWorkCycles(DateTime onDate, Expression<Func<WorkCycle, bool>> predicate);
    }
}