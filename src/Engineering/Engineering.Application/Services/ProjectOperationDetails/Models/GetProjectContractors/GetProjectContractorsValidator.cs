namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetProjectContractors;

public class GetProjectContractorsValidator : AbstractValidator<GetProjectContractorsRequest>
{
    public GetProjectContractorsValidator()
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