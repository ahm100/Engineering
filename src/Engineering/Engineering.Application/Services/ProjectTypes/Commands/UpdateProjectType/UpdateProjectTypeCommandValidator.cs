namespace Engineering.Application.Services.ProjectTypes.Commands.UpdateProjectType;

public class UpdateProjectTypeCommandValidator : AbstractValidator<UpdateProjectTypeCommand>
{
    public UpdateProjectTypeCommandValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectTypeErrors.IdIsEmpty);
        RuleFor(oo => oo.ProjectTypeName).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeNameIsEmpty);
        RuleFor(oo => oo.ProjectTypeCode).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ProjectTypeErrors.IsActiveIsEmpty);
    }
}