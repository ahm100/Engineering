namespace Engineering.Application.Services.CostOvers.Models.GetCostOverByName;

public class GetCostOverByNameValidator : AbstractValidator<GetCostOverByNameRequest>
{
    public GetCostOverByNameValidator()
    {
        RuleFor(v => v.CostOverName)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty);
    }
}