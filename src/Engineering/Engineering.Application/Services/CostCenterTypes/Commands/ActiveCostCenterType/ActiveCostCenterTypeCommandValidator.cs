namespace Engineering.Application.Services.CostCenterTypes.Commands.ActiveCostCenterType;

public class ActiveCostCenterTypeCommandValidator : AbstractValidator<ActiveCostCenterTypeCommand>
{
    public ActiveCostCenterTypeCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}