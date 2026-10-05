namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByCode;

public class GetCabinTypeByCodeQueryValidator : AbstractValidator<GetCabinTypeByCodeQuery>
{
    public GetCabinTypeByCodeQueryValidator()
    {
        RuleFor(v => v.CabinTypeCode)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}