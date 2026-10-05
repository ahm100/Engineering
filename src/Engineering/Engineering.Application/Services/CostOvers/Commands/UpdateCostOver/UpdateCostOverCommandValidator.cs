namespace Engineering.Application.Services.CostOvers.Commands.UpdateCostOver;

public class UpdateCostOverCommandValidator : AbstractValidator<UpdateCostOverCommand>
{
    public UpdateCostOverCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CostOverErrors.CostOverWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(v => v.CostOverName)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.CostOverCode)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}