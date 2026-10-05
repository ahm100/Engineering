namespace Engineering.Application.Services.CabinTypes.Models.GetCabinTypeById;

public class GetCabinTypeByIdValidator : AbstractValidator<GetCabinTypeByIdRequest>
{
    public GetCabinTypeByIdValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(CabinTypeErrors.IdIsEmpty);
    }
}