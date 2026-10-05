using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByIds;

public record GetsTelegramChatByIdsQuery(
    List<long> Ids
    ) : IQuery<List<TelegramChat>>;
