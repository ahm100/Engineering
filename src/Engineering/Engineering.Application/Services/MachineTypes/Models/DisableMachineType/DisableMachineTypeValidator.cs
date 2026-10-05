namespace Engineering.Application.Services.MachineTypes.Models.DisableMachineType;

public class DisableMachineTypeValidator : AbstractValidator<DisableMachineTypeRequest>
{
    public DisableMachineTypeValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}
