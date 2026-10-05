namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.DeleteMachineryConsumableVolume;

public class DeleteConsumableVolumeMachineryValidator : AbstractValidator<DeleteConsumableVolumeMachineryRequest>
{
    public DeleteConsumableVolumeMachineryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
