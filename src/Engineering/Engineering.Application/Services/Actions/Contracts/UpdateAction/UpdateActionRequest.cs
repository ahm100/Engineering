namespace Engineering.Application.Services.Actions.Contracts.UpdateAction;

public record UpdateActionRequest(
    long Id,
    string Name,
    string Code)
    : IHttpRequest;
