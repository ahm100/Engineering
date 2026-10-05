namespace Engineering.Application.Services.Machineries.Commands.DisableMachinery;

public class DisableMachineryCommandValidator : AbstractValidator<DisableMachineryCommand>
{
    public DisableMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(MachineryErrors.IdIsEmpty);
    }
}