namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectDetailData;

public class GetProjectDetailDataValidator : AbstractValidator<GetProjectDetailDataRequest>
{
    public GetProjectDetailDataValidator()
    {
        RuleFor(c => c.ProductGroupId)
            .IsPositive(GlobalCmts.ProductGroupId);
        RuleFor(c => c.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
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