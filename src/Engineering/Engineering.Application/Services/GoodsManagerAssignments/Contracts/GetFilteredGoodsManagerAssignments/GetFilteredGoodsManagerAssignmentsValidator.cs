namespace Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;

public class GetFilteredGoodsManagerAssignmentsValidator
    : AbstractValidator<GetFilteredGoodsManagerAssignmentsRequest>
{
    public GetFilteredGoodsManagerAssignmentsValidator()
    {

        RuleFor(x => x.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(x => x.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}