namespace Engineering.Application.Services.ProjectWarehouses.Contracts.GetProjectWarehouseAssets;

public class GetProjectWarehouseAssetsValidator : AbstractValidator<GetProjectWarehouseAssetsRequest>
{
    public GetProjectWarehouseAssetsValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);

        When(oo => oo.ProductGroupId is null && oo.ProductId is null, () =>
        {
            RuleFor(oo => oo.ProductGroupId).IsPositiveWithNullableInput(GlobalCmts.ProductGroupId);
        });

        RuleFor(oo => oo.ProductGroupId).IsOptionalPositive(GlobalCmts.ProductGroupId);
        RuleFor(oo => oo.ProductId).IsOptionalPositive(GlobalCmts.ProductId);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
    }
}
