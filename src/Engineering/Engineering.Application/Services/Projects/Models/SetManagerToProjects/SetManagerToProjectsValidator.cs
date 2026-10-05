namespace Engineering.Application.Services.Projects.Models.SetManagerToProjects;

public class SetManagerToProjectsValidator : AbstractValidator<SetManagerToProjectsRequest>
{
    public SetManagerToProjectsValidator()
    {
        RuleFor(oo => oo.ProjectIds).NotEmpty().WithError(ProjectErrors.IdsIsEmpty);
        RuleFor(oo => oo.ProjectManagerId).NotNull().WithError(ProjectErrors.ProjectManagerIdIsEmpty)
            .GreaterThanOrEqualTo(1).WithError(ProjectErrors.ProjectManagerIdGreaterThanZero);
    }
}
