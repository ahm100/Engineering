namespace Engineering.Application.Services.ConsumableVolumes.Models.Experts.CreateExpertConsumableVolume;

public class CreateConsumableVolumeExpertValidator : AbstractValidator<CreateConsumableVolumeExpertRequest>
{
    public CreateConsumableVolumeExpertValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ExpertId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeExpertErrors.ExpertIdIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeExpertErrors.FinalValueIsEmpty);
    }
}
