namespace Engineering.Application.Services.Projects.Models.GetProjectProductGroupByProjectId;

public class GetProjectProductGroupByProjectIdValidator : AbstractValidator<GetProjectProductGroupByProjectIdRequest>
{
    public GetProjectProductGroupByProjectIdValidator()
    {
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
        RuleFor(oo => oo.PageIndex).PageIndexZero(GlobalCmts.PageIndex);
        RuleFor(oo => oo.PageSize).PageSizeZero(GlobalCmts.PageSize);
    }
}
