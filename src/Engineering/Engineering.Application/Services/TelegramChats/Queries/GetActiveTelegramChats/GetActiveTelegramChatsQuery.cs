using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetActiveTelegramChats;

public record GetActiveTelegramChatsQuery(
    string? FilterData,
    long? CostCenterId,
    long? ProjectId,
    string? Name,
    bool? GetOther,
    int PageIndex,
    int PageSize
) : IQuery<DataResult<List<TelegramChat>>>;