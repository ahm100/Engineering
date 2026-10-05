namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectContractors;

public class GetProjectContractorsQueryValidator : AbstractValidator<GetProjectContractorsQuery>
{
    public GetProjectContractorsQueryValidator()
    {
        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.PageIndex)
            .PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize)
            .PageSizeZero(GlobalCmts.PageSize);
        When(oo => oo.PageSize > 0, () =>
        {
            RuleFor(c => c.PageIndex)
                .PageIndexOne(GlobalCmts.PageIndex);
        });
    }
}