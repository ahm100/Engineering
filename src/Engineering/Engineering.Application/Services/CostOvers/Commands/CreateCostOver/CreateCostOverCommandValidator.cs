namespace Engineering.Application.Services.CostOvers.Commands.CreateCostOver;

public class CreateCostOverCommandValidator : AbstractValidator<CreateCostOverCommand>
{
    public CreateCostOverCommandValidator()
    {
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