namespace Engineering.Application.Services.Projects.Models.DeleteProjectThirdParty;

public class DeleteProjectThirdPartyValidator : AbstractValidator<DeleteProjectThirdPartyRequest>
{
    public DeleteProjectThirdPartyValidator()
    {
        RuleForEach(oo => oo.Ids).IsPositive(ProjectCmts.ProjectId);
    }
}