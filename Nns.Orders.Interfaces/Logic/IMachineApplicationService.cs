using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Interfaces.Logic
{
    public interface IMachineApplicationService
    {
        Task<long> Add(CreateMachineApplicationRequest request);
        Task<MachineApplicationResponse> Get(long id);

        Task<bool> CanApply(long settlementId, long workKindId, long machineKindId);
    }
}