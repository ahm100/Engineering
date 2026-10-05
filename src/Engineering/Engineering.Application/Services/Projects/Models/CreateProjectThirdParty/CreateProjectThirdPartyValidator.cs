namespace Engineering.Application.Services.Projects.Models.AddAuthorizedThirdPartyToProject;

public class CreateProjectThirdPartyValidator : AbstractValidator<CreateProjectThirdPartyRequest>
{
    public CreateProjectThirdPartyValidator()
    {
        RuleForEach(oo => oo.ThirdPartyIds)
            .IsPositive(GlobalCmts.ThirdPartyId);

        RuleFor(oo => oo.ProjectId)
            .IsPositive(GlobalCmts.ProjectId);
    }
}
