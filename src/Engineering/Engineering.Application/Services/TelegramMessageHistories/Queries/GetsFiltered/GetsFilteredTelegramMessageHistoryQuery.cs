using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramMessageHistorys.Queries.GetsFiltered;

public record GetsFilteredTelegramMessageHistoryQuery(
    List<long>? Ids,
    string? FilterData,
    TelegramMessageType? MessageType,
    bool? IsSend,
    int PageIndex,
    int PageSize
) : IQuery<DataResult<List<TelegramMessageHistory>>>;