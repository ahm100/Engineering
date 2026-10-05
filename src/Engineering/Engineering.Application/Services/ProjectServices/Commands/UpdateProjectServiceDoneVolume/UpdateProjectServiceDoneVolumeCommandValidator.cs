namespace Engineering.Application.Services.ProjectServices.Commands.UpdateProjectServiceDoneVolume;

public class UpdateProjectServiceDoneVolumeCommandValidator : AbstractValidator<UpdateProjectServiceDoneVolumeCommand>
{
    public UpdateProjectServiceDoneVolumeCommandValidator()
    {
        RuleFor(oo => oo.Id)
            .NotNull().NotEmpty().WithError(ProjectServiceErrors.ProjectServiceIsEmpty);
    }
}
