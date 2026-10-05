namespace Engineering.Application.Services.CostCenterTypes.Commands.UpdateCostCenterType;

public class UpdateCostCenterTypeCommandValidator : AbstractValidator<UpdateCostCenterTypeCommand>
{
    public UpdateCostCenterTypeCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostCenterTypeErrors.CostCenterTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(v => v.CostCenterTypeName)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.CostCenterTypeCode)
            .NotEmpty().WithError(CostCenterTypeErrors.CostCenterTypeCodeIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}