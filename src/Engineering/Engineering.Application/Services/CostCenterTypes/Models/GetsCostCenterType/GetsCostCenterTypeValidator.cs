namespace Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterType;

public class GetsCostCenterTypeValidator : AbstractValidator<GetsCostCenterTypeRequest>
{
    public GetsCostCenterTypeValidator()
    {
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