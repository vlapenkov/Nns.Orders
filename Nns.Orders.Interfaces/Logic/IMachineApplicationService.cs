using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.Interfaces.Logic
{
    public interface IMachineApplicationService
    {
        Task<long> Add(CreateMachineApplicationRequest request);
        Task<MachineApplicationResponse> Get(long id);
                
    }
}