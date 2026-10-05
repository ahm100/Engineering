namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeByName;

public class GetCabinTypeByNameQueryValidator : AbstractValidator<GetCabinTypeByNameQuery>
{
    public GetCabinTypeByNameQueryValidator()
    {
        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty);
    }
}