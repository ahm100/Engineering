namespace Engineering.Application.Services.Projects.Commands.UpdateProject;

public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
{
    public UpdateProjectCommandValidator()
    {
        RuleFor(oo => oo.Project).NotNull().WithError(ProjectErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectName).NotEmpty().WithError(ProjectErrors.ProjectNameIsEmpty);
        RuleFor(oo => oo.Status).NotNull().WithError(GlobalErrors.StatusIsNull).IsInEnum().WithError(GlobalErrors.StatusNotInEnum);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ProjectErrors.IsActiveIsEmpty);
        RuleFor(oo => oo.Contractual).NotNull().WithError(ProjectErrors.ContractualIsEmpty);
    }
}