
namespace Engineering.Application.Services.ConsumableVolumes.Queries.Products.GetsProductsByFiltered;

public class GetsProductsByFilteredQueryValidator : AbstractValidator<GetsProductsByFilteredQuery>
{
    public GetsProductsByFilteredQueryValidator()
    {
        RuleFor(oo => oo.CostCenterId).NotNull().WithError(ConsumableVolumeProductErrors.CostCenterIdIsEmpty);
    }
}
