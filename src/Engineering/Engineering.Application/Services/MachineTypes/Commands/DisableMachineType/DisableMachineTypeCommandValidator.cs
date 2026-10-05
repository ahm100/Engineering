namespace Engineering.Application.Services.MachineTypes.Commands.DisableMachineType;

public class DisableMachineTypeCommandValidator : AbstractValidator<DisableMachineTypeCommand>
{
    public DisableMachineTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}