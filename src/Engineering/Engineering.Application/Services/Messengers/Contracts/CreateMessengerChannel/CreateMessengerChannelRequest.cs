using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.CreateMessengerChannel;

public record CreateMessengerChannelRequest(MessengerMessageType Type, string ChatId, string? ChatUrl, string? ChatName, long MessengerId) : IHttpRequest;