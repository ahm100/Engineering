using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Services.GetFltrMessengerChannels.Contracts.GetFltrMessengerChannel;

public record GetMessengerChannelsRequest(
    List<long>? ProjectIds,
    List<long>? CostCenterIds,
    MessengerMessageType? MessengerMessageType,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
