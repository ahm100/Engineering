
namespace Engineering.Application.Services.ServiceInfos.Models.StateChangerServiceInfos;

public class StateChangerServiceInfosValidator : AbstractValidator<StateChangerServiceInfosRequest>
{
    public StateChangerServiceInfosValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
