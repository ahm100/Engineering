namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;

public class GetReferenceTypeHistoryValidator : AbstractValidator<GetReferenceTypeHistoryRequest>
{
    public GetReferenceTypeHistoryValidator()
    {
        RuleFor(c => c.ReferenceId)
            .IsPositive(RGSCmts.ReferenceId);
        RuleFor(c => c.SupplyType)
            .IsEnum(RGSCmts.SupplyType);
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