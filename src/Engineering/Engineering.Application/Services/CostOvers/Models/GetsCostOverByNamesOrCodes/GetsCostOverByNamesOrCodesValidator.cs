namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverByNamesOrCodes;

public class GetsCostOverByNamesOrCodesValidator : AbstractValidator<GetsCostOverByNamesOrCodesRequest>
{
    public GetsCostOverByNamesOrCodesValidator()
    {
        RuleFor(v => v.Names)
            .NotEmpty().WithError(CostOverErrors.CostOverNameIsEmpty);

        RuleFor(v => v.Codes)
            .NotEmpty().WithError(CostOverErrors.CostOverCodeIsEmpty);
    }
}