namespace Engineering.Application.Services.CostCenters.Commands.InactiveCostCenter;

public class InactiveCostCenterCommandValidator : AbstractValidator<InactiveCostCenterCommand>
{
    public InactiveCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterErrors.IdIsEmpty);
    }
}