namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.UpdateExpertConsumableVolume;

public class UpdateConsumableVolumeExpertValidator : AbstractValidator<UpdateConsumableVolumeExpertRequest>
{
    public UpdateConsumableVolumeExpertValidator()
    {
        RuleFor(oo => oo.Id).NotNull().WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
    }
}
