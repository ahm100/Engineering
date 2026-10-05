namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.DeleteExpertConsumableVolume;

public class DeleteConsumableVolumeExpertValidator : AbstractValidator<DeleteConsumableVolumeExpertRequest>
{
    public DeleteConsumableVolumeExpertValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
    }
}
