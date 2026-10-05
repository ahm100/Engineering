namespace Engineering.Application.Services.ConsumableVolumes.Models.Machineries.GetsFilteredMachineriyVolume;

public class GetsFilteredMachineriyVolumeValidator : AbstractValidator<GetsFilteredMachineriyVolumeRequest>
{
    public GetsFilteredMachineriyVolumeValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeMachineryErrors.ProjectIdIsEmpty);
    }
}
