namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;

public class GetDetailByRGSTypeIdValidator : AbstractValidator<GetDetailByRGSTypeIdRequest>
{
    public GetDetailByRGSTypeIdValidator()
    {
        RuleFor(c => c.Id)
            .IsPositive(GlobalCmts.Id);
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