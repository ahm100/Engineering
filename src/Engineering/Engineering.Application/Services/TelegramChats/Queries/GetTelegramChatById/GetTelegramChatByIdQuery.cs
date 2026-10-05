using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatById;

public record GetTelegramChatByIdQuery(
    long Id
    ) : IQuery<TelegramChat?>;