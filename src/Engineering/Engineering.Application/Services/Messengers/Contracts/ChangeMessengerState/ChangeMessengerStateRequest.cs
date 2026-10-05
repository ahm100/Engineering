namespace Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;

public record ChangeMessengerStateRequest(
    List<long> Ids,
    bool IsActive
    ) : IHttpRequest;
