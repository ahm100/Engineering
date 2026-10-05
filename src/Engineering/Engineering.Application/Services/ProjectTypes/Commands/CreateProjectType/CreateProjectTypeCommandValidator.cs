namespace Engineering.Application.Services.ProjectTypes.Commands.CreateProjectType;

public class CreateProjectTypeCommandValidator : AbstractValidator<CreateProjectTypeCommand>
{
    public CreateProjectTypeCommandValidator()
    {
        RuleFor(oo => oo.ProjectTypeName).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeNameIsEmpty);
        RuleFor(oo => oo.ProjectTypeCode).NotEmpty().WithError(ProjectTypeErrors.ProjectTypeCodeIsEmpty);
        RuleFor(oo => oo.IsActive).NotNull().WithError(ProjectTypeErrors.IsActiveIsEmpty);
    }
}