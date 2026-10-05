namespace Engineering.Application.Services.ConsumableVolumes.Queries.Machineries.GetsFilteredMachineriyVolume;

public class GetsFilteredMachineriyVolumeQueryValidator : AbstractValidator<GetsFilteredMachineriyVolumeQuery>
{
    public GetsFilteredMachineriyVolumeQueryValidator()
    {
        RuleFor(oo => oo.ProjectId).NotNull().WithError(ConsumableVolumeMachineryErrors.ProjectIdIsEmpty);
    }
}
