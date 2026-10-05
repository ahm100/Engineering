using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.UpdateMessengerChannel;

public record UpdateMessengerChannelRequest(
    long Id,
    MessengerMessageType? Type,
    string? ChatId,
    string? ChatUrl,
    string? ChatName,
    long? MessengerId) : IHttpRequest;
