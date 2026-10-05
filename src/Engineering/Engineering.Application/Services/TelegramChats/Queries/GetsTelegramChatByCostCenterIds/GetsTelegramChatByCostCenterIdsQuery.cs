using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.TelegramChats.Enums;

namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;

public record GetsTelegramChatByCostCenterIdsQuery(
    List<long>? CostCenterIds,
    TelegramMessageType? TelegramMessageType,
    string? FilterData,
    bool? IsActive,
    bool? GetOther,
    int PageIndex,
    int PageSize
) : IQuery<DataResult<List<TelegramChat>>>;