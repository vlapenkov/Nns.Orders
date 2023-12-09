using FluentValidation;
using Nns.Orders.WebApi.Models;

namespace Nns.Orders.WebApi.Validators
{
    public class MachineApplicationRequestValidator :  AbstractValidator<CreateEquipmentApplicationRequest>
    {
        public MachineApplicationRequestValidator()
        {
            RuleFor(p => p.StartDate)
                .NotEmpty();            

            RuleFor(p => p.EquipmentTypeId)
               .NotEmpty();

            RuleFor(p => p.WorkTypeId)
               .NotEmpty();
        }
    }
}
