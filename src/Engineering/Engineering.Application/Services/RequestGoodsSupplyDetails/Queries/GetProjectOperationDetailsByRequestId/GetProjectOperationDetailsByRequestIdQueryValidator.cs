
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetProjectOperationDetailsByRequestId;

public class GetProjectOperationDetailsByRequestIdQueryValidator : AbstractValidator<GetProjectOperationDetailsByRequestIdQuery>
{
    public GetProjectOperationDetailsByRequestIdQueryValidator()
    {
        RuleFor(c => c.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleFor(c => c.ProductGroupId)
            .IsPositive(GlobalCmts.ProductGroupId);
        RuleFor(c => c.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(c => c.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(v => v.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}
