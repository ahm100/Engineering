namespace Engineering.Application.Services.ProjectOperations.Models.ChangeProject;

public class ChangeProjectValidator : AbstractValidator<ChangeProjectRequest>
{
    public ChangeProjectValidator()
    {
        RuleFor(oo => oo.Id).IsPositive(GlobalCmts.Id);
        RuleFor(oo => oo.ProjectId).IsPositive(GlobalCmts.ProjectId);
    }
}