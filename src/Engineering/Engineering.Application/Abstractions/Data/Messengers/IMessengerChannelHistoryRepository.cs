using Engineering.Application.Services.MessengerChannelHistories.Contracts.GetMessengerChannelHistories;
using Engineering.Domain.Entities.Messengers;
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Application.Abstractions.Data.Messengers;

public interface IMessengerChannelHistoryRepository : IBaseRepository<MessengerChannelHistory>
{
    Task<List<MessengerChannelHistory>?> GetFltrChannelHistory(
        List<long> ids,
        MessengerMessageType? messageType,
        bool? isSend, CT ct);

    Task<(List<GetMessengerChannelHistoriesModel> Data, int RowCount)> GetMessengerChannelHistories(
        List<long>? ids,
        long? targetId,
        MessengerMessageType? messageType,
        bool? isSend,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);


}