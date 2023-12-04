using FluentValidation;
using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.WebApi.Validators
{
    public class MachineApplicationRequestValidator :  AbstractValidator<CreateMachineApplicationRequest>
    {
        public MachineApplicationRequestValidator()
        {
            RuleFor(p => p.StartDate)
                .NotEmpty();

            RuleFor(p => p.SettlementId)
               .NotEmpty();

            RuleFor(p => p.MachineKindId)
               .NotEmpty();

            RuleFor(p => p.WorkKindId)
               .NotEmpty();
        }
    }
}
