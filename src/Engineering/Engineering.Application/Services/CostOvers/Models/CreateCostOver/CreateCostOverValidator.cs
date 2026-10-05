namespace Engineering.Application.Services.CostOvers.Models.CreateCostOver;

public class CreateCostOverValidator : AbstractValidator<CreateCostOverRequest>
{
    public CreateCostOverValidator()
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