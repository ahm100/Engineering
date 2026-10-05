namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCodes;

public class GetCabinTypeByCodesQueryValidator : AbstractValidator<GetCabinTypeByCodesQuery>
{
    public GetCabinTypeByCodesQueryValidator()
    {
        RuleForEach(v => v.CabinTypeCodes)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}