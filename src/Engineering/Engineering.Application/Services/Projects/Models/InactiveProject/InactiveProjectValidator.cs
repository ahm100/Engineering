namespace Engineering.Application.Services.Projects.Models.InactiveProject;

public class InactiveProjectValidator : AbstractValidator<InactiveProjectRequest>
{
    public InactiveProjectValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1).WithError(ProjectErrors.IdIsEmpty);
    }
}
