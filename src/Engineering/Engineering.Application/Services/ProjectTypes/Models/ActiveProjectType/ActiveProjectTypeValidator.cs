namespace Engineering.Application.Services.ProjectTypes.Models.ActiveProjectType;

public class ActiveProjectTypeValidator : AbstractValidator<ActiveProjectTypeRequest>
{
    public ActiveProjectTypeValidator()
    {
        RuleFor(oo => oo.Id)
            .IsPositive(ProjectCmts.ProjectTypeId);
    }
}
