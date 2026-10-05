namespace Engineering.Application.Services.ConsumableVolumes.Commands.Products.CreateProductConsumableVolume;

public class CreateConsumableVolumeProductCommandValidator : AbstractValidator<CreateConsumableVolumeProductCommand>
{
    public CreateConsumableVolumeProductCommandValidator()
    {
        RuleFor(oo => oo.ProjectOperationDetail).NotNull().WithError(ConsumableVolumeProductErrors.ProjectOperationDetailIdIsEmpty);
        RuleFor(oo => oo.ProductGroupId).NotNull().WithError(ConsumableVolumeProductErrors.ProductGroupIdIsEmptyIsEmpty);
        RuleFor(oo => oo.FinalValue).NotNull().WithError(ConsumableVolumeProductErrors.FinalValueIsEmpty);
        RuleFor(oo => oo.IsStandard).NotNull().WithError(ConsumableVolumeProductErrors.IsStandardIsEmpty);
        RuleFor(oo => oo.VolumeProductType)
            .IsInEnum().WithError(GlobalErrors.TypeNotInEnum)
            .NotNull().WithError(GlobalErrors.TypeIsNull);
    }
}
