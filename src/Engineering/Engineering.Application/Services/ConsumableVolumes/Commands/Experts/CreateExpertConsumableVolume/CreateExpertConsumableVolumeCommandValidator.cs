
namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.CreateExpertConsumableVolume;

public class CreateConsumableVolumeExpertCommandValidator : AbstractValidator<CreateConsumableVolumeExpertCommand>
{
    public CreateConsumableVolumeExpertCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotNull().WithError(ConsumableVolumeExpertErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ExpertId).NotNull().WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
    }
}