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
    public class EquipmentState : IEquipmentState
    {
        private readonly IOrderDbContext _dbContext;
        
        public EquipmentState(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EquipmentApplication?> GetApplication(DateTime onDate, long workTypeId, long equipmentTypeId)
        {
            return await _dbContext.EquipmentApplications
                .Where(p=>p.StartDate<=onDate)
                .Where(p=>p.WorkTypeId==workTypeId && p.EquipmentTypeId==equipmentTypeId)
                .OrderByDescending(e => e.StartDate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
        
    }
}
