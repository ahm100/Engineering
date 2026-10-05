namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetDetailPOWbsByProjectWbsId;

public class GetDetailPOWbsByProjectWbsIdQueryValidator : AbstractValidator<GetDetailPOWbsByProjectWbsIdQuery>
{
    public GetDetailPOWbsByProjectWbsIdQueryValidator()
    {
        RuleFor(c => c.ProjectWbsId)
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