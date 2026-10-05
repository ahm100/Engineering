namespace Engineering.Application.Services.CostCenterTypes.Commands.CreateCostCenterType;

public class CreateCostCenterTypeCommandValidator : AbstractValidator<CreateCostCenterTypeCommand>
{
    public CreateCostCenterTypeCommandValidator()
    {
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