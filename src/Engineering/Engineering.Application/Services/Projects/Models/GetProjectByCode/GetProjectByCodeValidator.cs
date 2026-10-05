namespace Engineering.Application.Services.Projects.Models.GetProjectByCode;

public class GetProjectByCodeValidator : AbstractValidator<GetProjectByCodeRequest>
{
    public GetProjectByCodeValidator()
    {
        RuleFor(oo => oo.ProjectCode).NotEmpty().WithError(ProjectErrors.ProjectCodeIsEmpty);
    }
}
