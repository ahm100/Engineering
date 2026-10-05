namespace Engineering.Application.Services.CabinTypes.Commands.DeleteCabinType;

public class DeleteCabinTypeCommandValidator : AbstractValidator<DeleteCabinTypeCommand>
{
    public DeleteCabinTypeCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);
    }
}