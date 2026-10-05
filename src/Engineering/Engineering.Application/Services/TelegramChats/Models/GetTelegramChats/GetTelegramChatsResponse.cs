using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

namespace Engineering.Application.Services.TelegramChats.Models.GetTelegramChats;

public record GetTelegramChatsResponse(
    List<GetTelegramChatsWithChildModel> Data,
    int RowCount
    );
