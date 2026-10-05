using Engineering.Domain.Entities.TelegramChats.Enums;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChats;

public record GetTelegramChatsQuery(
    List<long>? Ids,
    TelegramMessageType? TelegramMessageType,
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? Name,
    bool? IsActive,
    string[]? OrderBy,
    bool? GetOther,
    int PageIndex,
    int PageSize
) : IQuery<DataResult<List<TelegramChat>>>;