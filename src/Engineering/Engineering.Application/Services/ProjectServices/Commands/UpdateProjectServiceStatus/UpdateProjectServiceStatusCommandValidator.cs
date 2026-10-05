namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceStatus;

public class UpdateProjectServiceStatusCommandValidator : AbstractValidator<UpdateProjectServiceStatusCommand>
{
    public UpdateProjectServiceStatusCommandValidator()
    {
        RuleFor(oo => oo.ProjectService).NotNull().NotEmpty().WithError(ProjectServiceErrors.ProjectServiceIsEmpty);
    }
}
