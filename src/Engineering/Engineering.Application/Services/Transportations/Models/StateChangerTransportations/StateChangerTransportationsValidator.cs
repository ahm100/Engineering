
namespace Engineering.Application.Services.Transportations.Models.StateChangerTransportations;

public class StateChangerTransportationsValidator : AbstractValidator<StateChangerTransportationsRequest>
{
    public StateChangerTransportationsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
