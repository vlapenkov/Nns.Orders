using Nns.Orders.Domain.Documents;
using System.Linq.Expressions;

namespace Nns.Orders.Interfaces.Logic
{
    public interface IEquipmentState
    {
        Task<EquipmentApplication> GetApplication(DateTime onDate, long workTypeId, long equipmentTypeId);
    }
}