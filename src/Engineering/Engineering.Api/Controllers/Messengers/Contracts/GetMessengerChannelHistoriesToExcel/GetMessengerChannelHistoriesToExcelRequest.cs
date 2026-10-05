using Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelHistoriesToExcel;

namespace Engineering.Api.Controllers.Messengers.Contracts.MessengerChannelHistoriesExel;

public record GetMessengerChannelHistoriesToExcelRequest(
    long TargetId,
    List<MessengerChannelHistoryExcelColumn> ExcelFilters
    );

