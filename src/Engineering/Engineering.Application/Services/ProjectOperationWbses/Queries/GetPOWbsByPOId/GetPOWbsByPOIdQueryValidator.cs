namespace Engineering.Application.Services.ProjectOperationWbses.Queries.GetPOWbsByPOId;

public class GetPOWbsByPOIdQueryValidator : AbstractValidator<GetPOWbsByPOIdQuery>
{
    public GetPOWbsByPOIdQueryValidator()
    {
        RuleFor(oo => oo.ProjectOperationId)
            .IsPositive(GlobalCmts.ProjectOperation);
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