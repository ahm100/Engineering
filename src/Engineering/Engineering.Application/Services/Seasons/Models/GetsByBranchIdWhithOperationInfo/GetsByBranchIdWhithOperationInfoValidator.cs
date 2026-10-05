namespace Engineering.Application.Services.Seasons.Models.GetsByBranchIdWhithOperationInfo;

public class GetsByBranchIdWhithOperationInfoValidator : AbstractValidator<GetsByBranchIdWhithOperationInfoRequest>
{
    public GetsByBranchIdWhithOperationInfoValidator()
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
