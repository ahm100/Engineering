namespace Engineering.Application.Services.CabinTypes.Models.CreateCabinType;

public class CreateCabinTypeValidator : AbstractValidator<CreateCabinTypeRequest>
{
    public CreateCabinTypeValidator()
    {
        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);

        RuleFor(v => v.CabinTypeCode)
            .NotNull().WithError(CabinTypeErrors.CabinTypeCodeIsEmpty);
    }
}