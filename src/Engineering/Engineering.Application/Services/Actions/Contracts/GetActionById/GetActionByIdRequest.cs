namespace Engineering.Application.Services.Actions.Contracts.GetActionById;

public record GetActionByIdRequest(
    long Id)
    : IHttpRequest;
