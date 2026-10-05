namespace Engineering.Application.Services.ConsumableVolumes.Models.Products.GetProductConsumableVolumeById;

public class GetConsumableVolumeProductByIdValidator : AbstractValidator<GetConsumableVolumeProductByIdRequest>
{
    public GetConsumableVolumeProductByIdValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThan(0).WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
