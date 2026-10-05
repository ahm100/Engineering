namespace Engineering.Application.Services.Messengers.Contracts.GetMessengerById;

public record GetMessengerChannelByIdRequest(
    long Id
    ) : IHttpRequest;
