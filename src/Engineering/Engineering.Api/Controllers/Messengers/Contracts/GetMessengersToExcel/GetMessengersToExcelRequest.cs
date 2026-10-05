
using Engineering.Domain.Entities.Messengers.Enums;

namespace Engineering.Api.Controllers.Messengers.Contracts.GetMessengersToExcel;

public record GetMessengersToExcelRequest(
    long? TargetId,
    MessengerType? MessengerType,
    MessengerTargetType? MessengerTargetType,
    List<MessengersEnumExcel> ExcelFilters,
    int PageIndex,
    int PageSize
    );
