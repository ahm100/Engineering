namespace Engineering.Application.Services.CostOvers.Queries.GetsCostOverByNamesOrCodes;

public class GetsCostOverByNamesOrCodesQueryValidator : AbstractValidator<GetsCostOverByNamesOrCodesQuery>
{
    public GetsCostOverByNamesOrCodesQueryValidator()
    {
        RuleFor(v => v.Names)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty);

        RuleFor(v => v.Codes)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty);
    }
}