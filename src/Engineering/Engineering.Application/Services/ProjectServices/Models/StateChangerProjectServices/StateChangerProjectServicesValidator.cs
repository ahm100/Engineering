
namespace Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;

public class StateChangerProjectServicesValidator : AbstractValidator<StateChangerProjectServicesRequest>
{
    public StateChangerProjectServicesValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
