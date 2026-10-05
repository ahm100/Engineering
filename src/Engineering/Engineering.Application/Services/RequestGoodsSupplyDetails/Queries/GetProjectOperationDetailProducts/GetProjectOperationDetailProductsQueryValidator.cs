namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailProducts;

public class GetProjectOperationDetailProductsQueryValidator : AbstractValidator<GetProjectOperationDetailProductsQuery>
{
    public GetProjectOperationDetailProductsQueryValidator()
    {
        RuleFor(c => c.ProjectOperationDetailId)
            .IsPositive(GlobalCmts.ProjectOperationDetailId);
    }
}
