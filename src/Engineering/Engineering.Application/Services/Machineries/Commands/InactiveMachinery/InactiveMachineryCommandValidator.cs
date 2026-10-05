namespace Engineering.Application.Services.Machineries.Commands.InactiveMachinery;

public class InactiveMachineryCommandValidator : AbstractValidator<InactiveMachineryCommand>
{
    public InactiveMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}