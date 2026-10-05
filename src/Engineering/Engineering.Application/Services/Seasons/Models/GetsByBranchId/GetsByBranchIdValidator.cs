namespace Engineering.Application.Services.Seasons.Models.GetsByBranchId;

public class GetsByBranchIdValidator : AbstractValidator<GetsByBranchIdRequest>
{
    public GetsByBranchIdValidator()
    {
        RuleFor(oo => oo.BranchId)
            .IsPositive(GlobalCmts.BranchId);
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
