namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsProjectManagerRequestGoodsSupplie;

public class GetsProjectManagerRequestGoodsSupplieValidator : AbstractValidator<GetsProjectManagerRequestGoodsSupplieRequest>
{
    public GetsProjectManagerRequestGoodsSupplieValidator()
    {
        RuleFor(oo => oo.ProjectManagerId).NotNull().GreaterThanOrEqualTo(1).WithError(RequestGoodsSupplyManagementErrors.InValidProjectManagerId);
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
