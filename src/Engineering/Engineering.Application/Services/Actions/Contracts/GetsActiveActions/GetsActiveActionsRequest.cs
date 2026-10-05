namespace Engineering.Application.Services.Actions.Contracts.GetsActiveActions;

public record GetsActiveActionsRequest(
    string? FilterData,
    string? Code,
    string? Name,
    int PageIndex,
    int PageSize)
    : IHttpRequest;