namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetGoodsManagerAssignmentHistory;

public class GetGoodsManagerAssignmentHistoryValidator
    : AbstractValidator<GetGoodsManagerAssignmentHistoryRequest>
{
    public GetGoodsManagerAssignmentHistoryValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(GlobalCmts.Id);

        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}