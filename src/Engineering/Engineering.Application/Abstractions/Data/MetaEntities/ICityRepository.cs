using Engineering.Application.Services.Projects.Models.GetProjectById;
using Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewCityRepository : IBaseRepository<ViewCity>
{
    Task<ViewCity?> GetById(
        long id,
        CT ct);

    Task<MyViewCity?> GetCityById(
        long id, CT ct);

    Task<bool> IsDuplicateName(
        long? id,
        string name,
        CT ct);

    Task<bool> IsDuplicateCode(
        long? id,
        string code,
        CT ct);

    Task<bool> IsDuplicateEnglishName(
        long? id,
        string englishName,
        CT ct);

    Task<(List<ViewCity> Data, int RowCount)> GetFilteredCities(
        List<long>? ids,
        string? code,
        string? name,
        long? provinceId,
        string? filterData,
        bool? isActive,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewCity> Data, int RowCount)> GetCitiesByIds(
        List<long> ids,
        string? filterData,
        bool? ignoreQuery,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewCity> Data, int RowCount)> GetCities(
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewCity> Data, int RowCount)> GetAllActiveCities(
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<ViewCityDataModel> Data, int RowCount)> GetAllActiveCitiesData(
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<ViewCity>> GetByIds(
        List<long> ids,
        CT ct);


    Task<List<ViewCity>> GetByCodes(
        List<string> codes,
        CT ct);
}