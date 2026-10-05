using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;

public record GetMessengerChannelHistoriesRequest(
    List<long>? Ids,
    long TargetId,
    MessengerMessageType? MessageType,
    bool? IsSend,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
