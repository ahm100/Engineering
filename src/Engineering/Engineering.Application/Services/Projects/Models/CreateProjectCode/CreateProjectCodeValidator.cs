namespace Engineering.Application.Services.Projects.Models.CreateProjectCode;

public class CreateProjectCodeValidator : AbstractValidator<CreateProjectCodeRequest>
{
    public CreateProjectCodeValidator()
    {
        RuleFor(oo => oo.EmployerId).GreaterThan(0).WithError(ProjectErrors.EmployerIdIsEmpty);
    }
}
