namespace Engineering.Application.Services.Actions.Contracts.GetFilteredActions;

public record GetFilteredActionsRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;
