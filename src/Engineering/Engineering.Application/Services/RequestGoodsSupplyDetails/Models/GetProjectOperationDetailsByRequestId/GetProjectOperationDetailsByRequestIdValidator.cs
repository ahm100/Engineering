
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;

public class GetProjectOperationDetailsByRequestIdValidator : AbstractValidator<GetProjectOperationDetailsByRequestIdRequest>
{
    public GetProjectOperationDetailsByRequestIdValidator()
    {
        RuleFor(c => c.Type)
            .IsEnum(GlobalCmts.Type);
        RuleFor(c => c.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleFor(c => c.ProductGroupId)
            .IsPositive(GlobalCmts.ProductGroupId);
        RuleFor(c => c.RequestedCount)
            .IsPositive(RGSCmts.RequestedCount);
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
