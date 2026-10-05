namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.UpdateProductConsumableVolume;

public class UpdateConsumableVolumeProductValidator : AbstractValidator<UpdateConsumableVolumeProductRequest>
{
    public UpdateConsumableVolumeProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.IdIsEmpty);
        RuleFor(oo => oo.ProductGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.VolumeProductType)
               .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
               .NotNull().WithError(GlobalErrors.TypeIsNull);
    }
}
