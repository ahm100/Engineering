using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.GetCostCenterHistories;
using Engineering.Domain.Entities.CostCenters;

namespace Engineering.Persistence.Repositories.CostCenters;

public class CostCenterHistoryRepository : BaseRepository<EngineeringDBContext, CostCenterHistory>, ICostCenterHistoryRepository
{
    public CostCenterHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public Task<List<GetCostCenterHistoriesModel>> GetCostCenterHistories(
        long id,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(p => p.CostCenterId == id)
            .Select(x => new GetCostCenterHistoriesModel()
            {
                Id = x.Id,
                CostCenterTypeId = x.CostCenter.CostCenterTypesId,
                CostCenterCode = x.CostCenterCode,
                CostCenterTypeTitle = x.CostCenter.CostCenterType.CostCenterTypeTitle,
                CostCenterName = x.CostCenterName,
                NoOperationDays = x.NoOperationDays,
                Address = x.Address,
                PostalCode = x.PostalCode,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                Description = x.Description,
                WeatherState = x.WeatherState,
                IsActive = x.IsActive,
                CompanyId = x.CompanyId,
                PreferentialReferenceCode = x.PreferentialReferenceCode,
            });

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = query.ToListAsync();
        return newQuery;
    }
}
