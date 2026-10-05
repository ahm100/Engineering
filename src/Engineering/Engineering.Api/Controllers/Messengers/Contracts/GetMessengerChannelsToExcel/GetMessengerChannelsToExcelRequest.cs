using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Api.Controllers.Messengers.Contracts.GetMessengerChannelsToExcel;

public record GetMessengerChannelsToExcelRequest(
    List<long>? ProjectIds,
    List<long>? CostCenterIds,
    MessengerMessageType? MessengerMessageType,
    List<MessengerChannelsEnumExcel>? ExcelFilters,
    int PageIndex,
    int PageSize
    );
