
namespace Engineering.Application.Services.Projects.Models.StateChangerProjects;

public class StateChangerProjectsValidator : AbstractValidator<StateChangerProjectsRequest>
{
    public StateChangerProjectsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
