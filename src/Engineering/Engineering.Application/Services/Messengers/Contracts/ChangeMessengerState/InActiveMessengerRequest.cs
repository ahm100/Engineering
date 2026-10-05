namespace Engineering.Application.Services.Messengers.Contracts.ChangeMessengerState;

public record InActiveMessengerRequest(
    long Id
    ) : IHttpRequest;