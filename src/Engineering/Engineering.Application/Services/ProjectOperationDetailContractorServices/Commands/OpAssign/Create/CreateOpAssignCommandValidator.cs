using Engineering.Application.Services.GoodsManagerAssignments.Contracts.GetFilteredGoodsManagerAssignments;

public class GetFilteredGoodsManagerAssignmentsValidator
    : AbstractValidator<GetFilteredGoodsManagerAssignmentsRequest>
{
    public GetFilteredGoodsManagerAssignmentsValidator()
    {
        RuleForEach(x => x.Ids)
            .GreaterThan(0)
            .When(x => x.Ids is not null);

        RuleFor(x => x.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);

        RuleFor(x => x.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
    }
}