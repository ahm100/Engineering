using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

namespace Engineering.Application.Services.TelegramChats.Models.GetByCostCenterId;

public record GetByCostCenterIdResponse(
    List<GetTelegramChatsWithChildModel> Data,
    int RowCount
);