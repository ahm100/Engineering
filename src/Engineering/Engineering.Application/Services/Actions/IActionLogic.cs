using Engineering.Application.Services.Actions.Contracts.ActionCodeCreator;
using Engineering.Application.Services.Actions.Contracts.CreateAction;
using Engineering.Application.Services.Actions.Contracts.DeleteActions;
using Engineering.Application.Services.Actions.Contracts.GetActionById;
using Engineering.Application.Services.Actions.Contracts.GetFilteredActions;
using Engineering.Application.Services.Actions.Contracts.GetsActiveActions;
using Engineering.Application.Services.Actions.Contracts.UpdateAction;

namespace Engineering.Application.Services.Actions;

public interface IActionLogic
{
    Task<Result<CreateActionResponse?>> CreateAction(
        CreateActionRequest request, CT ct);

    Task<Result<DeleteActionResponse?>> DeleteAction(
        DeleteActionRequest request, CT ct);

    Task<Result<UpdateActionResponse?>> UpdateAction(
        UpdateActionRequest request, CT ct);

    Task<Result<GetActionByIdResponse?>> GetActionById(
        GetActionByIdRequest request, CT ct);

    Task<Result<GetsActiveActionsResponse?>> GetsActiveActions(
        GetsActiveActionsRequest request, CT ct);

    Task<Result<GetFilteredActionsResponse?>> GetFilteredActions(
        GetFilteredActionsRequest request, CT ct);

    Task<Result<ActionCodeCreatorResponse?>> ActionCodeCreator(
        ActionCodeCreatorRequest request, CT ct);
}
