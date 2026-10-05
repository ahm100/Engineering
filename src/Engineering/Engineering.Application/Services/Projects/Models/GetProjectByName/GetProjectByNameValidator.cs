namespace Engineering.Application.Services.Projects.Models.GetProjectByName;

public class GetProjectByNameValidator : AbstractValidator<GetProjectByNameRequest>
{
    public GetProjectByNameValidator()
    {
        RuleFor(oo => oo.ProjectName).NotEmpty().WithError(ProjectErrors.ProjectNameIsEmpty);
    }
}
