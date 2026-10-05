namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.DeleteProductConsumableVolume;

public class DeleteConsumableVolumeProductCommandValidator : AbstractValidator<DeleteConsumableVolumeProductCommand>
{
    public DeleteConsumableVolumeProductCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
