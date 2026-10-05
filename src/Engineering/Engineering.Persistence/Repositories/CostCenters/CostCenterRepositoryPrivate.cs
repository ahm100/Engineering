using Engineering.Application.Abstractions.Data.CostCenters;
using Engineering.Application.Services.CostCenters.Models.CostCenterModels;
using CostCenter = Engineering.Domain.Entities.CostCenters.CostCenter;

namespace Engineering.Persistence.Repositories.CostCenters;

public partial class CostCenterRepository : BaseRepository<EngineeringDBContext, CostCenter>, ICostCenterRepository
{
    private IQueryable<GetCostCentersModel> BuildQueryGetCostCenters(
        List<long>? ids,
        string? filterData,
        string? costCenterName,
        string? costCenterCode,
        long? costCenterTypeId,
        long? informedUserId,
        long? authorizedRoleId,
        long? authorizedUserId,
        long? warehouseId,
        long? cityId,
        bool? isActive,
        long? companyId)
    {
        var query = DbSet.AsQueryable();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var newQuery = query.Where(oo => (companyId == null || oo.CompanyId == companyId) &&
                        (costCenterCode == null || oo.CostCenterCode.Contains(costCenterCode)) &&
                        (costCenterName == null || oo.CostCenterName.Contains(costCenterName)) &&
                        (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterCode, filterData.MakeLikePattern()) ||
                         string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(oo.CostCenterName, filterData.MakeLikePattern())) &&
                        (costCenterTypeId == null || oo.CostCenterType!.Id == costCenterTypeId) &&
                        (informedUserId == null || oo.InformedUsers.Any(x => x.EmployeeId == informedUserId)) &&
                        (authorizedRoleId == null || oo.CostCenterAuthorizedRoles.Any(x => x.AuthorizedRoleId == authorizedRoleId)) &&
                        (authorizedUserId == null || oo.CostCenterAuthorizedUsers.Any(x => x.AuthorizedUserId == authorizedUserId)) &&
                        (warehouseId == null || oo.CostCenterWarehouses.Any(x => x.WarehouseId == warehouseId)) &&
                        (cityId == null || oo.CityId == cityId) &&
                        (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                        (isActive == null || oo.IsActive == isActive))

            .Select(x => new GetCostCentersModel
            {
                Id = x.Id,
                Address = x.Address,
                CityId = x.CityId,
                CompanyId = x.CompanyId,
                CostCenterCode = x.CostCenterCode,
                CostCenterName = x.CostCenterName,
                CostCenterEnName = x.CostCenterEnName,
                CostCenterTypeTitle = x.CostCenterType.CostCenterTypeTitle,
                CostCenterWarehouseId = x.CostCenterWarehouses.Any(x => x.IsDefault) ? x.CostCenterWarehouses.Where(x => x.IsDefault).FirstOrDefault().Id : x.CostCenterWarehouses.FirstOrDefault().Id,
                WarehouseId = x.CostCenterWarehouses.Any(x => x.IsDefault) ? x.CostCenterWarehouses.Where(x => x.IsDefault).FirstOrDefault().WarehouseId : x.CostCenterWarehouses.FirstOrDefault().WarehouseId,
                Created = x.Created,
                Description = x.Description,
                DescriptionEn = x.DescriptionEn,
                IsActive = x.IsActive,
                IsDefault = x.IsDefault,
                Latitude = x.Latitude,
                Longitude = x.Longitude,
                NoOperationDays = x.NoOperationDays,
                PostalCode = x.PostalCode,
                WeatherState = x.WeatherState,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        newQuery = newQuery.OrderByDescending(x => x.IsDefault);

        return newQuery;
    }
}