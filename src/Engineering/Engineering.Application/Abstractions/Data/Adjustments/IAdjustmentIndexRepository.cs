using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexById;
using Engineering.Application.Services.Adjustments.Contracts.GetAdjustmentIndexes;
using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Application.Abstractions.Data.Adjustments;

public interface IAdjustmentIndexRepository : IBaseRepository<AdjustmentIndex>
{
    Task AddRangeAsync(IEnumerable<AdjustmentIndex> adjustmentIndexes, CT ct);

    Task<AdjustmentIndex?> GetById(
      long id,
      CT ct);

    Task<GetAdjustmentIndexByIdResponse?> GetByIdForApi(
        long id,
        CT ct);

    Task<GetAdjustmentIndexesResponse> GetAdjustmentIndexes(
        GetAdjustmentIndexesRequest request,
        CT ct);

    Task<bool> ExistsByReferenceAndCode(
        long adjustmentReferenceId,
        string code,
        long? excludeId,
        CT ct);
}

