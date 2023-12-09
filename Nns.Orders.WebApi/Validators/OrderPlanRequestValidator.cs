using FluentValidation;
using Nns.Orders.WebApi.Models;

namespace Nns.Orders.WebApi.Validators;

public class OrderPlanRequestValidator : AbstractValidator<CreateWorkOrderRequest>
{
    public OrderPlanRequestValidator()
    {
        RuleFor(p => p.StartDate)
            .NotEmpty();

        RuleFor(p => p.WorkTypeId)
            .NotEmpty();

        RuleFor(p => p.EquipmentTypeId)
            .NotEmpty();

        RuleFor(p => p.Value)
            .Must(p => p <= 100);
    }
}