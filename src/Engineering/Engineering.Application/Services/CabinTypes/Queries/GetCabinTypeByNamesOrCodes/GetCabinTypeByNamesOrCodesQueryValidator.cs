namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByNamesOrCodes;

public class GetCabinTypeByNamesOrCodesQueryValidator : AbstractValidator<GetCabinTypeByNamesOrCodesQuery>
{
    public GetCabinTypeByNamesOrCodesQueryValidator()
    {
        RuleForEach(v => v.Names)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty);

        RuleForEach(v => v.Codes)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}