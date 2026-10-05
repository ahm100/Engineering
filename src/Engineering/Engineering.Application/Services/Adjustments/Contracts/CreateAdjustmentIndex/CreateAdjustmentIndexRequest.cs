using Engineering.Domain.Entities.Adjustment.Enums;
using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Application.Services.Adjustments.Contracts.CreateAdjustmentIndex;

public record CreateAdjustmentIndexRequest(
    long AdjustmentReferenceId,
    long YearId,
    long BranchId,
    long? SeasonId,
    string Code,
    string Title,
    string? Description,
    string? DocumentFile,
    List<CreateAdjustmentIndexValueRequest> Values);

public record CreateAdjustmentIndexValueRequest(
    string YearName,
    ContractAdjustmentPeriod Period,
    AdjustmentIndexType Type,
    decimal Value,
    decimal? Coefficient,
    string? NotificationNumber,
    DateTime? NotificationDate,
    bool IsActive);

