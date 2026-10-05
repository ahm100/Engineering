
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;

public record GetsFilteredTelegramMessageHistoryRequest(
    List<long>? Ids,
    string? FilterData,
    TelegramMessageType? MessageType,
    bool? IsSend,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
