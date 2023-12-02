using FluentValidation;
using Nns.Orders.Interfaces.Models;

namespace Nns.Orders.WebApi.Validators
{
    public class OrderPlanRequestValidator :   AbstractValidator<CreateOrderPlanRequest>
    {
        public OrderPlanRequestValidator()
        {
            RuleFor(p => p.StartDate)
                .NotEmpty()
                .Must(p => p.Date == p)
                .Must(p => p.Kind == DateTimeKind.Utc);

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
