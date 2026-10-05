using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;

public record UpdateAdjustmentIndexValueRequest(
    long? Id,
    string YearName,
    ContractAdjustmentPeriod Period,
    AdjustmentIndexType Type,
    decimal Value,
    decimal? Coefficient,
    string? NotificationNumber,
    DateTime? NotificationDate,
    bool IsActive);