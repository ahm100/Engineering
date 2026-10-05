
namespace Engineering.Application.Services.OperationInfos.Models.StateChangerOperationInfos;

public class StateChangerOperationInfosValidator : AbstractValidator<StateChangerOperationInfosRequest>
{
    public StateChangerOperationInfosValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
