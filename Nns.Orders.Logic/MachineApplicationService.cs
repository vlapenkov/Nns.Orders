using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;
using Nns.Orders.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Logic
{
    public class MachineApplicationService : IMachineApplicationService
    {
        private IOrderDbContext _dbContext;

        public MachineApplicationService(IOrderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<long> Add(CreateMachineApplicationRequest request)
        {

            if(await _dbContext.MachineApplications.AnyAsync(p =>p.SettlementId==p.SettlementId && p.StartDate > request.StartDate))
                throw  new AppException("Нельзя вводить данные задним числом. Уже есть более поздние записи.");

            bool hasApplication = await _dbContext.MachineApplications.AnyAsync(p =>
            p.StartDate == request.StartDate &&
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

        public async Task<MachineApplicationResponse> Get(long id)
        {
            var result = await _dbContext.MachineApplications
                .Include(p => p.WorkKind)
                .Include(p => p.MachineKind)
                .Include(p => p.Settlement)
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new NotFoundException($"Сущность не найдена по ключу {id}");

            return new MachineApplicationResponse
            {
                Id = result.Id,
                MachineKind = new MachineKindDto { Id = result.Id, Name = result.MachineKind.Name },
                Settlement = new SettlementDto { Id = result.SettlementId, Name = result.Settlement.Name },
                WorkKind = new WorkKindDto { Id = result.WorkKind.Id, Name = result.WorkKind.Name },
                IsActive = result.IsActive
            };


        }

      
    }
}
