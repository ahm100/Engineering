namespace Engineering.Application.Services.MachineTypes.Commands.ActiveMachineType;

public class ActiveMachineTypeCommandValidator : AbstractValidator<ActiveMachineTypeCommand>
{
    public ActiveMachineTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}