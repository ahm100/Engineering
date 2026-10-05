namespace Engineering.Application.Services.MachineTypes.Models.InactiveMachineType;

public class InactiveMachineTypeValidator : AbstractValidator<InactiveMachineTypeRequest>
{
    public InactiveMachineTypeValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}
