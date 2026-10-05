namespace Engineering.Application.Services.Messengers.Contracts.DeleteMessenger;

public record DeleteMessengerRequest(long Id) : IHttpRequest;
