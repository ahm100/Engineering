namespace Engineering.Application.Services.CabinTypes.Commands.UpdateCabinType;

public class UpdateCabinTypeCommandValidator : AbstractValidator<UpdateCabinTypeCommand>
{
    public UpdateCabinTypeCommandValidator()
    {
        RuleFor(v => v.Id)
            .NotNull().WithError(CabinTypeErrors.CabinTypeWithIdNotFound)
            .GreaterThanOrEqualTo(1).WithError(GlobalErrors.IdMustGreaterZiro);

        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}