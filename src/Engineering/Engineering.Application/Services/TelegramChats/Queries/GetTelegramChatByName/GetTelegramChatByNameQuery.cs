using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatByName;

public record GetTelegramChatByNameQuery(
    string ChatName
) : IQuery<TelegramChat?>;