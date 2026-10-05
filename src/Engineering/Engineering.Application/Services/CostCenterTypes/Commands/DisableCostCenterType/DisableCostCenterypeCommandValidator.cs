namespace Engineering.Application.Services.CostCenterTypes.Commands.DisableCostCenterType;

public class DisableCostCenterTypeCommandValidator : AbstractValidator<DisableCostCenterTypeCommand>
{
    public DisableCostCenterTypeCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}