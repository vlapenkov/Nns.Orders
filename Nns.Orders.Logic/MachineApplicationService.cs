using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Logic
{
    public class MachineApplicationService
    {
        private IOrderDbContext _dbContext;

        public MachineApplicationService(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Add(CreateMachineApplicationRequest request) {
                        
            
            bool hasApplication =  await _dbContext.MachineApplications.AnyAsync(p =>
            p.StartDate ==request.StartDate &&
            p.SettlementId == request.SettlementId
            && p.MachineKindId == request.MachineKindId
            && p.WorkKindId == request.WorkKindId            
            );

            if (hasApplication) throw new AppException("Применяемость по данным измерениям уже установлена");

            var model = new MachineApplication
            {
                StartDate = request.StartDate,
                MachineKindId = request.MachineKindId,
                SettlementId = request.SettlementId,
                WorkKindId = request.WorkKindId,
                IsActive = request.IsActive
            };

            _dbContext.MachineApplications.Add(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }
    }
}
