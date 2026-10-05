
namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.UpdateProductConsumableVolume;

public class UpdateConsumableVolumeProductCommandValidator : AbstractValidator<UpdateConsumableVolumeProductCommand>
{
    public UpdateConsumableVolumeProductCommandValidator()
    {
        RuleFor(oo => oo.ConsumableVolumeProduct).NotNull().WithError(ConsumableVolumeProductErrors.IdIsEmpty);
        RuleFor(oo => oo.ProductGroupId).NotNull().WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.IsStandard).NotNull().WithError(ConsumableVolumeProductErrors.IsStandardIsEmpty);
        RuleFor(oo => oo.VolumeProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(GlobalErrors.TypeIsNull);
    }
}
