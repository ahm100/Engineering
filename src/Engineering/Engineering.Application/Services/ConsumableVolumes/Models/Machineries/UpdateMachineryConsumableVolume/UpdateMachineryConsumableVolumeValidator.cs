namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.UpdateMachineryConsumableVolume;

public class UpdateConsumableVolumeMachineryValidator : AbstractValidator<UpdateConsumableVolumeMachineryRequest>
{
    public UpdateConsumableVolumeMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeMachineryErrors.IdIsEmpty);
        RuleFor(oo => oo.MachineryId).GreaterThanOrEqualTo(1).NotNull().WithError(ConsumableVolumeMachineryErrors.MachineryIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeMachineryErrors.FinalValueIsEmpty);
    }
}
