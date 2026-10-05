namespace Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;

public record ActiveMessengerRequest(
    long Id
    ) : IHttpRequest;