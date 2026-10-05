using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewRegionRepository : IBaseRepository<ViewRegion>
{
    Task<ViewRegion?> GetById(
        long id,
        CT ct);

    Task<List<ViewRegion>> GetByIds(
        List<long> ids,
        CT ct);

    Task<List<ViewRegion>> GetByCodes(
        List<string> codes,
        CT ct);

    Task<(List<ViewRegionDataModel> Data, int RowCount)> GetAllActiveRegionsData(
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);
}