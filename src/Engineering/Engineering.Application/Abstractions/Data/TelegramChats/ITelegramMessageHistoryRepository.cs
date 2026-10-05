using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Abstractions.Data.TelegramChats;

public interface ITelegramMessageHistoryRepository : IBaseRepository<TelegramMessageHistory>
{
    Task<(List<TelegramMessageHistory> Data, int RowCount)> GetsFilteredTelegramMessageHistory(
        List<long>? ids,
        string? filterData,
        TelegramMessageType? messageType,
        bool? isSend,
        int pageIndex,
        int pageSize,
        CT ct);
}