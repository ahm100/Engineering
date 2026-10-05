using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.UpdateMessenger;

public record UpdateMessengerRequest(
    long Id,
    MessengerType? Type,
    MessengerTargetType? TargetType,
    long? TargetId,
    bool? IsActive,
    string? Description) : IHttpRequest;
