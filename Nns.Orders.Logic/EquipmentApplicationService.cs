
using Microsoft.EntityFrameworkCore;
using Nns.Orders.Common.Exceptions;
using Nns.Orders.Domain.Documents;
using Nns.Orders.Domain.Entities;
using Nns.Orders.Interfaces;
using Nns.Orders.Interfaces.Logic;


namespace Nns.Orders.Logic
{
    public class EquipmentApplicationService : IEquipmentApplicationService
    {
        private readonly IOrderDbContext _dbContext;
        private readonly IEquipmentState _equipmentState;

        public EquipmentApplicationService(IOrderDbContext dbContext, IEquipmentState equipmentState)
        {
            _dbContext = dbContext;
            _equipmentState = equipmentState;
        }

        public async Task<long> Add(EquipmentApplication model)
        {
            await CheckIfRetro(model);

            await CheckExistanceOnDate(model);

            await CheckOrdersLater(model);

            await CheckCurrentActivity(model);

            _dbContext.EquipmentApplications.Add(model);

            await _dbContext.SaveChangesAsync();

            return model.Id;
        }

        private async Task CheckCurrentActivity(EquipmentApplication model)
        {
            var lastRecord = await _equipmentState.GetApplication(model.StartDate, model.WorkTypeId,model.EquipmentTypeId);


            if (model.IsActive && lastRecord != null && lastRecord.IsActive)
            {
                throw new AppException($"Применяемость с WorkTypeId={model.WorkTypeId} и EquipmentTypeId={model.EquipmentTypeId} уже активна на {lastRecord.StartDate}");
            }

            if (!model.IsActive && (lastRecord == null || !lastRecord.IsActive))
            {
                throw new AppException($"Применяемость с WorkTypeId={model.WorkTypeId} и EquipmentTypeId={model.EquipmentTypeId}  отсутствует или не активна");
            }
        }

        private async Task CheckExistanceOnDate(EquipmentApplication model)
        {
            bool hasApplication = await _dbContext.EquipmentApplications.AnyAsync(p =>
            p.StartDate == model.StartDate
            && p.EquipmentTypeId == model.EquipmentTypeId
            && p.WorkTypeId == model.WorkTypeId
            );

            if (hasApplication)
            {
                throw new AppException($"Применяемость по данным измерениям уже установлена на дату {model.StartDate}");
            }
        }

        private async Task CheckIfRetro(EquipmentApplication model)
        {
            if (await _dbContext.EquipmentApplications.AnyAsync(p => p.StartDate.Date > model.StartDate))
            {
                throw new AppException("Нельзя вводить данные задним числом. Уже есть более поздние записи.");
            }
        }

        private async Task CheckOrdersLater(EquipmentApplication model)
        {
            if (await _dbContext.WorkOrders.AnyAsync(p => p.StartDate.Date > model.StartDate))
            {
                throw new AppException("Есть задания позднее текущего применения.");
            }
        }

        public async Task<EquipmentApplication> Get(long id)
        {
            EquipmentApplication result = await _dbContext.EquipmentApplications
                .Include(p => p.WorkType)
                .Include(p => p.EquipmentType)
                .FirstOrDefaultAsync(p=>p.Id==id)
                ?? throw new EntityNotFoundException($"Сущность не найдена по ключу {id}");

            return result;
        }


    }
}
