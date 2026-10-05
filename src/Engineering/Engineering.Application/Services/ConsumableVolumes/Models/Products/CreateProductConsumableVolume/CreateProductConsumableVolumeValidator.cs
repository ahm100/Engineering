namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.CreateProductConsumableVolume;

public class CreateConsumableVolumeProductValidator : AbstractValidator<CreateConsumableVolumeProductRequest>
{
    public CreateConsumableVolumeProductValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetailId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ProductGroupId).NotNull().GreaterThanOrEqualTo(1).WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.VolumeProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(GlobalErrors.TypeIsNull);
    }
}
