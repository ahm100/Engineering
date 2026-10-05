using Engineering.Application.Services.TelegramChats.Models.TelegramChatModels;

namespace Engineering.Application.Services.TelegramChats.Models.GetsTelegramChatByCostCenterIds;

public record GetsTelegramChatByCostCenterIdsResponse(
    List<GetsActiveTelegramChatModel> Data,
    int RowCount
);