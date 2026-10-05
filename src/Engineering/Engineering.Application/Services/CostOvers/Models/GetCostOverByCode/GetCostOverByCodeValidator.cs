namespace Engineering.Application.Services.CostOvers.Models.GetCostOverByCode;

public class GetCostOverByCodeValidator : AbstractValidator<GetCostOverByCodeRequest>
{
    public GetCostOverByCodeValidator()
    {
        RuleFor(v => v.CostOverCode)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty);
    }
}