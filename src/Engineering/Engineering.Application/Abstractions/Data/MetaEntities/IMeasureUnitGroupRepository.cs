using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IMeasureUnitGroupRepository : IBaseRepository<MeasureUnitGroup>
{
    Task<MeasureUnitGroup?> GetByIdAsync(long id, CT ct);

    Task<List<MeasureUnitGroup>?> GetByGroupIdAsync(long groupId, CT ct);

    Task CreateOrUpdateAsync(MeasureUnitGroup measureUnitGroup, CT ct);
}