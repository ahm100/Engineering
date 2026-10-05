namespace Engineering.Application.Services.MachineriesGroups.Commands.DisableMachineriesGroup;

public class DisableMachineriesGroupCommandValidator : AbstractValidator<DisableMachineriesGroupCommand>
{
    public DisableMachineriesGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineriesGroupErrors.IdIsEmpty);
    }
}