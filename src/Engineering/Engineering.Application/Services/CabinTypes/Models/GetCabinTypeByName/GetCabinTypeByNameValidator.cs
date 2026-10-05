namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeByName;

public class GetCabinTypeByNameValidator : AbstractValidator<GetCabinTypeByNameRequest>
{
    public GetCabinTypeByNameValidator()
    {
        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty);
    }
}