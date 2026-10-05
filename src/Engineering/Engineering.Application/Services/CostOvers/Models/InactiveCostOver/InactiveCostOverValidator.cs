namespace Engineering.Application.Services.CostOvers.Models.InactiveCostOver;

public class InactiveCostOverValidator : AbstractValidator<InactiveCostOverRequest>
{
    public InactiveCostOverValidator()
    {
        RuleFor(v => v.Id).IsPositive(GlobalCmts.Id);
    }
}