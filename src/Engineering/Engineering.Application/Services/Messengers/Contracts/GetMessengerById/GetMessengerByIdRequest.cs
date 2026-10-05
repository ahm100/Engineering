namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public record GetMessengerByIdRequest(
    long Id
    ) : IHttpRequest;
