
namespace Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;

public record GetAdjustmentIndexesRequest(
    long? AdjustmentReferenceId,
    long? BranchId,
    long? SeasonId,
    string? Search,
    int? PageIndex,
    int? PageSize);


public record GetAdjustmentIndexesResponse(
    List<GetAdjustmentIndexesModel> Data,
    int Count);