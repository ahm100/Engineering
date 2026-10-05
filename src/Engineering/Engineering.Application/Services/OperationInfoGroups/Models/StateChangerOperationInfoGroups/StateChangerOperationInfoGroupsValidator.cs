
namespace Engineering.Application.Services.OperationInfoGroups.Models.StateChangerOperationInfoGroups;

public class StateChangerOperationInfoGroupsValidator : AbstractValidator<StateChangerOperationInfoGroupsRequest>
{
    public StateChangerOperationInfoGroupsValidator()
    {
        RuleFor(oo => oo.Ids).NotNull().WithError(GlobalErrors.IdsIsEmpty);
    }
}
