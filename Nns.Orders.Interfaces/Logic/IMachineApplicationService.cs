using Nns.Orders.Domain.Documents;


namespace Nns.Orders.Interfaces.Logic
{
    public interface IEquipmentApplicationService
    {
        Task<long> Add(EquipmentApplication model);
        Task<EquipmentApplication> Get(long id);
                
    }
}