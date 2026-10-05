using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.TelegramChats.Models.SendMessage;

public record SendMessageRequest(
    long TargetId,
    MessengerTargetType MessengerTargetType,
    MessengerType MessengerType,
    MessengerMessageType MessengerMessageType,
    string Message,
    List<Guid>? Urls
    ) : IHttpRequest;