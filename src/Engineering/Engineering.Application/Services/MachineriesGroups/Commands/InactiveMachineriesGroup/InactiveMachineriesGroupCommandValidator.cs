namespace Engineering.Application.Services.MachineriesGroups.Commands.InactiveMachineriesGroup;

public class InactiveMachineriesGroupCommandValidator : AbstractValidator<InactiveMachineriesGroupCommand>
{
    public InactiveMachineriesGroupCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineriesGroupErrors.IdIsEmpty);
    }
}