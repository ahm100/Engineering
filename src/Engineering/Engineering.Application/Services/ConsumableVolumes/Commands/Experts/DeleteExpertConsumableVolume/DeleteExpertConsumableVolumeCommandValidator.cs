
namespace Engineering.Application.Services.ConsumableVolumes.Commands.Experts.DeleteExpertConsumableVolume;

public class DeleteConsumableVolumeExpertCommandValidator : AbstractValidator<DeleteConsumableVolumeExpertCommand>
{
    public DeleteConsumableVolumeExpertCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ConsumableVolumeExpertErrors.IdIsEmpty);
    }
}
