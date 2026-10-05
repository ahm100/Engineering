namespace Engineering.Application.Services.MachineTypes.Commands.InactiveMachineType;

public class InactiveMachineTypeCommandValidator : AbstractValidator<InactiveMachineTypeCommand>
{
    public InactiveMachineTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineTypeErrors.IdIsEmpty);
    }
}