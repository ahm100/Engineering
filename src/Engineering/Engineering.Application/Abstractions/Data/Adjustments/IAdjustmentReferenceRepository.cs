using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Application.Abstractions.Data.Adjustments;

public interface IAdjustmentReferenceRepository
    : IBaseRepository<AdjustmentReference>
{
    Task<AdjustmentReference?> GetByTitle(string title, CT ct);
    Task<AdjustmentReference?> GetById(long id, CT ct);
}