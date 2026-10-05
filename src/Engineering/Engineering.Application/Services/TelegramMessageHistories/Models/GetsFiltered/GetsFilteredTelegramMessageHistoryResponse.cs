
namespace Engineering.Application.Services.TelegramMessageHistorys.Models.GetsFiltered;

public record GetsFilteredTelegramMessageHistoryResponse(
    List<GetsFilteredTelegramMessageHistoryResponseModel> Data,
    int RowCount
    );
