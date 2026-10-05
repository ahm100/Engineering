using Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetConfirmedProjectGoodsSupply;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.SetSingleConfirmedProjectGoodsSupply;

public class SetSingleConfirmedProjectGoodsSupplyDetailValidator : AbstractValidator<SetSingleConfirmedProjectGoodsSupplyDetail>
{
    public SetSingleConfirmedProjectGoodsSupplyDetailValidator()
    {
        RuleFor(oo => oo.RequestGoodsSupplyProductId).NotNull().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetail);

        When(oo => oo.BetweenStocks != null, () =>
        {
            RuleForEach(oo => oo.BetweenStocks).NotEmpty().SetValidator(new GoodsSupplyManagementModelBetweenStockValidator());
        });

        When(oo => oo.Commerce != null, () =>
        {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            RuleFor(oo => oo.Commerce).NotEmpty().SetValidator(new GoodsSupplyManagementModelCommerceValidator());
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
        });

        When(oo => oo.InStocks != null, () =>
        {
            RuleForEach(oo => oo.InStocks).NotEmpty().SetValidator(new GoodsSupplyManagementModelInStockValidator());
        });

        When(oo => oo.InStocks == null && oo.Commerce == null && oo.BetweenStocks == null, () =>
        {
            RuleFor(oo => oo.InStocks == null).NotNull().NotEmpty().WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyDetails);
        });
    }
}
