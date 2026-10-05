using Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IMeasureUnitRepository : IBaseRepository<MeasureUnit>
{
    Task<MeasureUnit?> GetByIdAsync(long id, CT ct);
    Task<MeasureUnit?> GetByIdIgnoreAsync(long groupId, CT ct);

    Task<List<MeasureUnit?>> GetByGroupIdAsync(long groupId, CT ct);

    Task CreateOrUpdateAsync(MeasureUnit measureUnit, CT ct);

    Task<List<MeasureUnit?>> GetAllByIdAsync(long id, CT ct);

    Task<List<MeasureUnit>> GetMeasureUnitsByIds(List<long> ids,
      CT ct);

    Task<List<MeasureUnit>> GetByNames(
        List<string> names,
        long companyId,
        CT ct);
    Task<List<MeasureUnitDto>> GetByNamesForFehrestBaha(List<string> names, CT ct);

    Task<MeasureUnit?> GetByName(
        string name,
        CT ct);
        
}