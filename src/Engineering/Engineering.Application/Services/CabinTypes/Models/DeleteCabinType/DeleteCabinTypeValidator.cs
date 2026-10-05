namespace Engineering.Application.Services.CabinTypes.Models.DeleteCabinType;

public class DeleteCabinTypeValidator : AbstractValidator<DeleteCabinTypeRequest>
{
    public DeleteCabinTypeValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}