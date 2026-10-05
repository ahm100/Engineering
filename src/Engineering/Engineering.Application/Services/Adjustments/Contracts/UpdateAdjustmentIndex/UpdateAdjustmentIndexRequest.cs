

namespace Engineering.Application.Services.Adjustments.Contracts.UpdateAdjustmentIndex;

public record UpdateAdjustmentIndexRequest(
    long Id,
    long AdjustmentReferenceId,
    long? YearId,
    long BranchId,
    long? SeasonId,
    string Code,
    string Title,
    string? Description,
    string? DocumentFile,
    bool IsActive,
    List<UpdateAdjustmentIndexValueRequest> Values);



public record UpdateAdjustmentIndexResponse(
    bool IsSuccess);