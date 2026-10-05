namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByCode;

public class GetCabinTypeByCodeValidator : AbstractValidator<GetCabinTypeByCodeRequest>
{
    public GetCabinTypeByCodeValidator()
    {
        RuleFor(v => v.CabinTypeCode)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}