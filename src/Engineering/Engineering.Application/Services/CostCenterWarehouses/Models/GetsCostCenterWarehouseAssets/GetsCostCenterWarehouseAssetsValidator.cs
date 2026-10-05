
namespace Engineering.Application.Services.CostCenterWarehouses.Models.GetsCostCenterWarehouseAssets;

public class GetsCostCenterWarehouseAssetsValidator : AbstractValidator<GetsCostCenterWarehouseAssetsRequest>
{
    public GetsCostCenterWarehouseAssetsValidator()
    {
        RuleFor(oo => oo.CostCenterId)
            .IsPositive(GlobalCmts.CostCenterId);

        When(oo => oo.ProductGroupId == null && oo.ProductId == null, () =>
        {
            RuleFor(oo => oo.ProductGroupId)
            .IsPositiveWithNullableInput(GlobalCmts.ProductGroupId);
        });

        RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageIndexNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexNotValid);
        RuleFor(oo => oo.PageSize).GreaterThanOrEqualTo(GlobalErrors.Zero).WithError(GlobalErrors.PageSizeNotValid)
            .LessThanOrEqualTo(GlobalErrors.MaxSize).WithError(GlobalErrors.PageSizeNotValid);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(oo => oo.PageIndex).GreaterThanOrEqualTo(GlobalErrors.One).WithError(GlobalErrors.PageIndexRequired)
                .LessThanOrEqualTo(GlobalErrors.MaxIndex).WithError(GlobalErrors.PageIndexRequired);
        });
    }
}
