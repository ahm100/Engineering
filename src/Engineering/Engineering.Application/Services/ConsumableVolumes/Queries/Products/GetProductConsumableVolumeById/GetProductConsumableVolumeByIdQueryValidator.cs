namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetProductConsumableVolumeById;

public class GetConsumableVolumeProductByIdQueryValidator : AbstractValidator<GetConsumableVolumeProductByIdQuery>
{
    public GetConsumableVolumeProductByIdQueryValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ConsumableVolumeProductErrors.IdIsEmpty);
    }
}
