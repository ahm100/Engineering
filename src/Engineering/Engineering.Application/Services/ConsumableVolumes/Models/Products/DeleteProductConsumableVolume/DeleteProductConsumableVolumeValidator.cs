namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.DeleteProductConsumableVolume;

public class DeleteConsumableVolumeProductValidator : AbstractValidator<DeleteConsumableVolumeProductRequest>
{
    public DeleteConsumableVolumeProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
