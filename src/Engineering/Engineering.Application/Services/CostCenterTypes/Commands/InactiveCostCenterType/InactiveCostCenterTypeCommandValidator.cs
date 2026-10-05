namespace Engineering.Application.Services.CostCenterTypes.Commands.InactiveCostCenterType;

public class InactiveCostCenterTypeCommandValidator : AbstractValidator<InactiveCostCenterTypeCommand>
{
    public InactiveCostCenterTypeCommandValidator()
    {
        RuleFor(v => v.Entity.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}