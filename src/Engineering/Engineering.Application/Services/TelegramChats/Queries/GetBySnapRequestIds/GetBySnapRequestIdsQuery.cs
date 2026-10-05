using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Queries.GetBySnapRequestIds;

public record GetBySnapRequestIdsQuery(
    List<long> SnapRequestIds,
    bool? GetOther,
    TelegramMessageType telegramMessageType
    ) : IQuery<DataResult<List<TelegramChat>>>;