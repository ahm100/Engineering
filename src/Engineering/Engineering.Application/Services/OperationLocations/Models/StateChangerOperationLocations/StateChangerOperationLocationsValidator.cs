
namespace Engineering.Application.Services.OperationLocations.Models.StateChangerOperationLocations;

public class StateChangerOperationLocationsValidator : AbstractValidator<StateChangerOperationLocationsRequest>
{
    public StateChangerOperationLocationsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
