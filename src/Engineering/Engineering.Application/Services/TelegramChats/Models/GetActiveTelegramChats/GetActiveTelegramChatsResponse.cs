using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

namespace Engineering.Application.Services.TelegramChats.Models.GetActiveTelegramChats;

public record GetActiveTelegramChatsResponse(
    List<GetsActiveTelegramChatModel> Data,
    int RowCount
    );

