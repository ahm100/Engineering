namespace Engineering.Application.Services.Actions.Contracts.DeleteActions;
public record DeleteActionRequest(
    long Id)
    : IHttpRequest;
