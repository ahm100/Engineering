using Engineering.Application.Services.Messengers.Contracts.GetMessengerById;
using Engineering.Application.Services.Messengers.Contracts.GetMessengers;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Abstractions.Data.Messengers;

public interface IMessengerChannelRepository : IBaseRepository<MessengerChannel>
{
    Task<MessengerChannel?> GetById(
        long id, CT ct);

    Task<MessengerChannel?> GetBestChannel(
        long targetId,
        MessengerTargetType messengerTargetType,
        MessengerType messengerType,
        MessengerMessageType messengerMessageType, CT ct);

    Task<List<MessengerChannel>?> GetChannelByProjectIdAndCostCenterId(
        List<long> projectIds,
        List<long> costCenterIds,
        MessengerMessageType messengerMessageType,
        string chatId, CT ct);

    Task<(List<GetMessengerChannelsModel> Data, int RowCount)> GetMessengerChannels(
        List<long>? projectIds,
        List<long>? costCenterIds,
        MessengerMessageType? messengerMessageType,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<MessengerChannel>?> GetFltrChannel(
        List<long>? projectIds,
        List<long>? costCenterIds,
        MessengerMessageType messengerMessageType, CT ct);


    Task<GetMessengerChannelByIdResponse?> GetMessengerChannelById(
    long id, CT ct);
}