namespace Engineering.Application.Services.Actions.Contracts.CreateAction;

public record CreateActionRequest(
    string Name,
    string Code
    ) : IHttpRequest;