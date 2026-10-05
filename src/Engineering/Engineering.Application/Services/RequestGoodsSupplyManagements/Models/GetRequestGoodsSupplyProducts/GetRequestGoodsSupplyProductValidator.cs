namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetRequestGoodsSupplyProducts;

public class GetRequestGoodsSupplyProductValidator : AbstractValidator<GetRequestGoodsSupplyProductRequest>
{
    public GetRequestGoodsSupplyProductValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyErrors.InValidRequestGoodsSupplyId);
        When(oo => oo.Type != null, () =>
        {
            RuleFor(oo => oo.Type).IsInEnum().WithError(RequestGoodsSupplyManagementErrors.InValidRequestGoodsSupplyType);
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
