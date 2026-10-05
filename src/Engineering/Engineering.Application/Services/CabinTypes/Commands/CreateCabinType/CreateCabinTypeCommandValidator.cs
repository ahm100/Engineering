namespace Engineering.Application.Services.CabinTypes.Commands.CreateCabinType;

public class CreateCabinTypeCommandValidator : AbstractValidator<CreateCabinTypeCommand>
{
    public CreateCabinTypeCommandValidator()
    {
        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.CabinTypeCode)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}