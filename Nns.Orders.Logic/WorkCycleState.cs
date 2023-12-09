using Microsoft.EntityFrameworkCore;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Logic
{
    public class WorkCycleState : IWorkCycleState
    {
        private readonly IOrderDbContext _dbContext;

        public WorkCycleState(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<WorkCycle[]> GetWorkCycles(DateTime onDate, Expression<Func<WorkCycle,bool>> predicate )
        {
            var workCycles = await _dbContext.WorkCycles.Where(p => p.StartDate <= onDate).AsNoTracking().ToListAsync();

            WorkCycle[] activeWorkCycles = workCycles
                .GroupBy(x => x.WorkTypeId, (key, g) => g.OrderByDescending(e => e.StartDate).First())
                .Where(predicate.Compile())
                .OrderBy(p => p.OrderNumber)
                .ToArray();

            return activeWorkCycles;
        }
        
    }
}
