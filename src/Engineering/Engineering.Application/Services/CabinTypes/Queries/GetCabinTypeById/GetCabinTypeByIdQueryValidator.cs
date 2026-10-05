namespace Engineering.Application.Services.CabinTypes.Queries.GetCabinTypeById;

public class GetCabinTypeByIdQueryValidator : AbstractValidator<GetCabinTypeByIdQuery>
{
    public GetCabinTypeByIdQueryValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}