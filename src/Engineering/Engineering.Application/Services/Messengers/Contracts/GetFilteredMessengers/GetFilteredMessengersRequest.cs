using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.Messengers.Contracts.GetMessengers;

public record GetMessengersRequest(
    long? TargetId,
    MessengerType? MessengerType,
    MessengerTargetType? MessengerTargetType,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
