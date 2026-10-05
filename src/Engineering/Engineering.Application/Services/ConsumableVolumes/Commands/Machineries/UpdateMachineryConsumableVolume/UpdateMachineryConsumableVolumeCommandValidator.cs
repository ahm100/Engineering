namespace Engineering.Application.Services.ConsumableVolumes.Commands.Machineries.UpdateMachineryConsumableVolume;

public class UpdateConsumableVolumeMachineryCommandValidator : AbstractValidator<UpdateConsumableVolumeMachineryCommand>
{
    public UpdateConsumableVolumeMachineryCommandValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeMachinery).NotNull().WithError(ConsumableVolumeMachineryErrors.IdIsEmpty);
        RuleFor(oo => oo.Machinery).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.IsStandard).NotNull().WithError(ConsumableVolumeMachineryErrors.IsStandardIsEmpty);
    }
}
