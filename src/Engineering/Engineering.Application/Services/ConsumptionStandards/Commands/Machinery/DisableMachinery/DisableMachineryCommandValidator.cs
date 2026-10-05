namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.DisableMachinery;

public class DisableMachineryCommandValidator : AbstractValidator<DisableMachineryCommand>
{
    public DisableMachineryCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(MachineryStandardErrors.MachineryIdIsEmpty);
    }
}