using Engineering.Domain.Entities.Synonyms.MetaData.Legals;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewLegalRepository : IBaseRepository<ViewLegal>
{
    Task<(List<ViewLegal> Data, int RowCount)> GetLegalsByIds(List<long> ids, int pageIndex, int pageSize,
        CT ct);

    Task<(List<ViewLegal> Data, int RowCount)> GetAllActiveLegals(string? filterData, int pageIndex, int pageSize,
        CT ct);

    Task<ViewLegal?> GetById(long id, CT ct);
}