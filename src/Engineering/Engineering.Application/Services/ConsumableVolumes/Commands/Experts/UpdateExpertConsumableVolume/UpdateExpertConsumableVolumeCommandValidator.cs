
namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.UpdateExpertConsumableVolume;

public class UpdateConsumableVolumeExpertCommandValidator : AbstractValidator<UpdateConsumableVolumeExpertCommand>
{
    public UpdateConsumableVolumeExpertCommandValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeExpert).NotNull().WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.IsStandard).NotNull().WithError(ConsumableVolumeExpertErrors.IsStandardIsEmpty);
    }
}
