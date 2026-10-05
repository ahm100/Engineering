
namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;

public class GetsProjectOperationDetailDataValidator : AbstractValidator<GetsProjectOperationDetailDataRequest>
{
    public GetsProjectOperationDetailDataValidator()
    {
        RuleFor(c => c.ProductGroupId)
            .IsPositive(GlobalCmts.ProductGroupId);
        RuleFor(c => c.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperationId);
        RuleFor(c => c.Type)
            .IsEnum(GlobalCmts.Type);
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
