using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.CreateMessenger;

public record CreateMessengerRequest(
    MessengerType Type,
    MessengerTargetType TargetType,
    long TargetId,
    bool IsActive,
    string? Description) : IHttpRequest;
