namespace Engineering.Application.Services.ProjectOperationWbses.Contracts.GetFltrPOWbs;

public class GetFltrPOWbsValidator : AbstractValidator<GetFltrPOWbsRequest>
{
    public GetFltrPOWbsValidator()
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