
namespace Engineering.Application.Services.Projects.Models.ActiveProject;

public class ActiveProjectValidator : AbstractValidator<ActiveProjectRequest>
{
    public ActiveProjectValidator()
    {
        RuleFor(oo => oo.Id).NotNull().GreaterThanOrEqualTo(1)
            .WithError(ProjectErrors.IdIsEmpty);
    }
}
