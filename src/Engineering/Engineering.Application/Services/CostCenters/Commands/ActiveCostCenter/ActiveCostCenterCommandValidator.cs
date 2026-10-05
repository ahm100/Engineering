namespace Engineering.Application.Services.CostCenters.Commands.ActiveCostCenter;

public class ActiveCostCenterCommandValidator : AbstractValidator<ActiveCostCenterCommand>
{
    public ActiveCostCenterCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(CostCenterErrors.IdIsEmpty);
    }
}