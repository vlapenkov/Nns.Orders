using FluentValidation;
using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.WebApi.Validators
{
    public class OrderPlanRequestValidator :   AbstractValidator<CreateOrderPlanRequest>
    {
        public OrderPlanRequestValidator()
        {
            RuleFor(p => p.StartDate)
                .NotEmpty();
                

            RuleFor(p => p.SettlementId)
                   .NotEmpty();

            RuleFor(p => p.MachineKindId)
                   .NotEmpty();

            RuleFor(p => p.WorkKindId)
                   .NotEmpty();

            RuleFor(p => p.Value)
                .Must(p => p <= 100 && p >= 0);
                

        }
    }
}
