namespace Engineering.Application.Services.CostOvers.Models.DisableCostOver;

public class DisableCostOverValidator : AbstractValidator<DisableCostOverRequest>
{
    public DisableCostOverValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}