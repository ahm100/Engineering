namespace Engineering.Application.Services.CabinTypes.Models.UpdateCabinType;

public class UpdateCabinTypeValidator : AbstractValidator<UpdateCabinTypeRequest>
{
    public UpdateCabinTypeValidator()
    {
        RuleFor(v => v.Id)
            .NotNull()
            .GreaterThanOrEqualTo(1)
            .WithError(CabinTypeErrors.IdIsEmpty);

        RuleFor(v => v.CabinTypeName)
            .NotEmpty().WithError(CabinTypeErrors.CabinTypeNameIsEmpty)
            .MaximumLength(250).WithError(GlobalErrors.MaxCharIs250)
            .Matches(@"^[^!#@&^$%~]+$")!.WithError(GlobalErrors.Regex);
    }
}