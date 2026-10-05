using Engineering.Application.Abstractions.Data.MetaEntities;
using Engineering.Application.IdentityServices.Roles.Models;
using Engineering.Application.IdentityServices.Roles.Queries.GetsRoleById;
using Engineering.Application.IdentityServices.Users.Queries.GetsUserById;
using Engineering.Application.IdentityServices.Users.Queries.GetUsersByRoleIds;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Application.WebServices.MetaDataServices.Banks.Models;
using Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetBankById;
using Engineering.Application.WebServices.MetaDataServices.Banks.Queries.GetsBankById;
using Engineering.Application.WebServices.MetaDataServices.Cities.Models;
using Engineering.Application.WebServices.MetaDataServices.Cities.Queries.GetsCityById;
using Engineering.Application.WebServices.MetaDataServices.Companies.Models;
using Engineering.Application.WebServices.MetaDataServices.Companies.Queries.GetFilteredCompaniesByIds;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Models;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostCategories.Queries.GetsCostCategoryById;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Models;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetCostGroupById;
using Engineering.Application.WebServices.MetaDataServices.CostGroups.Queries.GetsCostGroupById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Models;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Currencies.Queries.GetsCurrencyById;
using Engineering.Application.WebServices.MetaDataServices.Employers.Queries.GetEmployerById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Models;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.Measureunits.Queries.GetsMeasureunitById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetFilteredSkillByIds;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetSkillById;
using Engineering.Application.WebServices.MetaDataServices.Skills.Queries.GetsSkillById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredByIds;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetFilteredUsers;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetsThirdPartyById;
using Engineering.Application.WebServices.MetaDataServices.ThirdParties.Queries.GetWithSkillOnlyByIds;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Models.DownloadMultipleStaticFilesByNameStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFiles;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleFileStream;
using Engineering.Application.WebServices.ObjectStorageServices.Files.Queries.DownloadMultipleStaticFilesByNameStream;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Queries.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.Products.Queries.GetsFilteredProducts;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetsWarehouseCategoryById;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Queries.GetWarehouseCategoryById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.InventoryPackages.Models.GetsPackagesByIds;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.InventoryPackages.Queries.GetsPackagesByIds;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetById;
using Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Queries.GetsById;
using EmployerModel = Engineering.Application.WebServices.MetaDataServices.Employers.Models.Employer;
using IdentityUser = Engineering.Application.IdentityServices.Users.Models.User;
using ThirdPartyAlias = Engineering.Application.WebServices.MetaDataServices.ThirdParties.Models.ThirdParty;
using ViewMeasureUnit = Engineering.Domain.Entities.Synonyms.MetaData.MeasureUnits.MeasureUnit;
using WarehouseEntity = Gita.Backend.Shared.Application.WebServices.WarehouseServices.Warehouses.Models.Warehouse;

namespace Engineering.Application.Services.WebServices;

public static class WebServicesLogic
{
    public static async Task<DownloadMultipleStaticFilesByNameStreamsModelValue?> DownloadMultipleStaticFilesByNameStream(List<string> names, IMediator _mediator, CT ct)
    {
        DownloadMultipleStaticFilesByNameStreamsModelValue? result = null;
        var response = await _mediator.Send(new DownloadMultipleStaticFilesByNameStreamQuery(names, SubSystemType.Engineering), ct);
        if (response.IsSuccess)
            result = response.Value;

        return result;
    }

    public static async Task<DownloadMultipleFileStreamsModelValue?> DownloadMultipleFileStream(List<Guid>? ids, IMediator _mediator, CT ct)
    {
        DownloadMultipleFileStreamsModelValue? result = null;
        if (ids is not null && ids.Any())
        {
            var response = await _mediator.Send(new DownloadMultipleFileStreamQuery(ids, false), ct);
            if (response.IsSuccess)
                result = response.Value;
        }
        return result;
    }

    public static async Task<List<FileDownloaded>?> DownloadMultipleFiles(List<Guid>? ids, IMediator _mediator, CT ct)
    {
        List<FileDownloaded>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var responses = await _mediator.Send(new DownloadMultipleFilesQuery(ids, false), ct);
            result = responses.Value?.Data;
        }
        return result;
    }


    public static async Task<List<Currency>?> CurrenciesDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Currency>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var currenciesData = await _mediator.Send(new GetsCurrencyByIdQuery(1, ids.Count, ids, true), ct);
            result = currenciesData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<ThirdPartyAlias>?> ThirdPartiesDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<ThirdPartyAlias>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var thirdPartiesData = await _mediator.Send(new GetsThirdPartyByIdQuery(1, ids.Count, ids, true), ct);
            result = thirdPartiesData.Value?.Data!;
        }
        return result;
    }

    public static async Task<List<FilteredUserModel>?> GetFilteredByIdsDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<FilteredUserModel>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var usersData = await _mediator.Send(new GetFilteredByIdsQuery(ids, filterData, 1, ids.Count), ct);
            result = usersData.Value?.Data!;
        }
        return result;
    }

    public static async Task<List<IdentityUser>?> GetUsersByRoleIdsDataReceiver(List<long>? roleIds, List<long>? userIds, string? filterData, IMediator _mediator, CT ct)
    {
        List<IdentityUser>? result = [];
        if (roleIds is not null && roleIds.Count > 0 && userIds is not null && userIds.Count > 0)
        {
            var usesrData = await _mediator.Send(new GetUsersByRoleIdsQuery(roleIds, userIds, filterData, 1, userIds.Count), ct);
            result = usesrData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<IdentityUser>?> GetUsersDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<IdentityUser>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var usesrData = await _mediator.Send(new GetsUserByIdQuery(ids), ct);
            result = usesrData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<Company>?> CompaniesDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Company>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var currenciesData = await _mediator.Send(new GetFilteredCompaniesByIdsQuery(ids, null, 1, ids.Count), ct);
            result = currenciesData.Value?.Data;
        }
        return result;
    }

    public static async Task<Company?> CompanyDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        Company? result = null;
        if (id is not null)
        {
            var companyData = await _mediator.Send(new GetCompanyByIdQuery((long)id), ct);
            result = companyData.Value;
        }
        return result;
    }

    public static async Task<EmployerModel?> EmployerDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        EmployerModel? result = null;
        if (id is not null)
        {
            var employerData = await _mediator.Send(new GetEmployerByIdQuery((long)id), ct);
            result = employerData.Value;
        }
        return result;
    }

    public static async Task<Skill?> SkillDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        Skill? result = null;
        if (id is not null)
        {
            var skillData = await _mediator.Send(new GetSkillByIdQuery((long)id), ct);
            result = skillData.Value;
        }
        return result;
    }

    public static async Task<Bank?> BankDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        Bank? result = null;
        if (id is not null)
        {
            var bankData = await _mediator.Send(new GetBankByIdQuery((long)id), ct);
            result = bankData.Value;
        }
        return result;
    }

    public static async Task<List<Bank>?> BanksDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Bank>? result = null;
        if (ids is not null && ids.Count > 0)
        {
            var bankIds = ids.Adapt<List<long?>>().Distinct().ToList();
            var bankData = await _mediator.Send(new GetsBankByIdQuery(1, bankIds.Count, bankIds, true), ct);
            result = bankData.Value?.Data;
        }
        return result;
    }


    public static async Task<CostCategory?> CostCategoryDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        CostCategory? result = null;
        if (id is not null)
        {
            var bankData = await _mediator.Send(new GetCostCategoryByIdQuery((long)id), ct);
            result = bankData.Value;
        }
        return result;
    }

    public static async Task<List<CostCategory>?> CostCategoriesDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<CostCategory>? result = null;
        if (ids is not null && ids.Count > 0)
        {
            var bankIds = ids.Adapt<List<long>>().Distinct().ToList();
            var bankData = await _mediator.Send(new GetsCostCategoryByIdQuery(bankIds, null, null, null, 1, bankIds.Count), ct);
            result = bankData.Value?.Data;
        }
        return result;
    }


    public static async Task<CostGroup?> CostGroupDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        CostGroup? result = null;
        if (id is not null)
        {
            var bankData = await _mediator.Send(new GetCostGroupByIdQuery((long)id), ct);
            result = bankData.Value;
        }
        return result;
    }

    public static async Task<List<CostGroup>?> CostGroupsDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<CostGroup>? result = null;
        if (ids is not null && ids.Count > 0)
        {
            var bankIds = ids.Adapt<List<long>>().Distinct().ToList();
            var bankData = await _mediator.Send(new GetsCostGroupByIdQuery(bankIds, null, null, 1, bankIds.Count), ct);
            result = bankData.Value?.Data;
        }
        return result;
    }

    public static async Task<GetProductModel?> ProductDataReceiver(long? id, IMediator _mediator, IViewProductRepository _pRepo, CT ct)
    {
        GetProductModel? result = null;
        if (id is not null)
        {
            var productData = await _pRepo.GetProductByIds([id.Value], ct);
            result = productData?.FirstOrDefault();
        }
        return result;
    }

    public static async Task<Currency?> CurrencyDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        Currency? result = null;
        if (id is not null)
        {
            var currencyData = await _mediator.Send(new GetCurrencyByIdQuery((long)id), ct);
            result = currencyData.Value;
        }
        return result;
    }

    public static async Task<Measureunit?> MeasureUnitDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        Measureunit? result = null;
        if (id is not null)
        {
            var measureData = await _mediator.Send(new GetMeasureunitByIdQuery((long)id), ct);
            result = measureData.Value;
        }
        return result;
    }

    public static async Task<List<WarehouseEntity>?> WarehousesDataReceiver(List<long?> ids, IMediator _mediator, CT ct)
    {
        List<WarehouseEntity>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var warehouseData = await _mediator.Send(new GetsWarehouseByIdQuery(1, ids.Count, ids), ct);
            result = warehouseData.Value?.Data;
        }
        return result;
    }

    public static async Task<WarehouseEntity?> WarehouseDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        WarehouseEntity? result = null;
        if (id is not null)
        {
            var warehouseData = await _mediator.Send(new GetWarehouseByIdQuery((long)id), ct);
            result = warehouseData.Value;
        }
        return result;
    }

    public static async Task<List<UserModel?>?> GetWithSkillOnlyByIdsReceiver(List<long>? ids, string? filterData, bool? isActive, IMediator _mediator, CT ct)
    {
        List<UserModel?>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var newIds = ids.Distinct().ToList();
            var userData = await _mediator.Send(new GetWithSkillOnlyByIdsQuery(1, newIds.Count, newIds, filterData, true, isActive), ct);
            result = userData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<FilteredUserResponseModel>?> UserDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<FilteredUserResponseModel>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var usersData = await _mediator.Send(new GetFilteredUsersQuery(ids, filterData, null, null, null, 1, ids.Count), ct);
            result = usersData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<Group>?> GroupsDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Group>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var currenciesData = await _mediator.Send(new GetGroupsByIdsQuery(ids, 1, ids.Count), ct);
            result = currenciesData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<GetsPackagesByIdsModel>?> PackagesDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<GetsPackagesByIdsModel>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var getPackageByIdQuery = await _mediator.Send(new GetsPackagesByIdsQuery(ids, 1, ids.Count), ct);
            result = getPackageByIdQuery.Value?.Data!;
        }
        return result;
    }

    public static async Task<List<Skill>?> SkillsDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Skill>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var skillData = await _mediator.Send(new GetsSkillByIdQuery(ids, true, 1, ids.Count), ct);
            result = skillData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<Skill>?> GetFilteredSkillsDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<Skill>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var skillData = await _mediator.Send(new GetFilteredSkillByIdsQuery(ids, filterData, 1, ids.Count), ct);
            result = skillData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<FilteredGroup>?> GetFilteredGroupsDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<FilteredGroup>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var groupData = await _mediator.Send(new GetFilteredGroupsByIdsQuery(ids, filterData, null, 1, ids.Count), ct);
            result = groupData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<GetProductModel>?> ProductsDataReceiver(List<long>? ids, IMediator _mediator, IViewProductRepository _pRepo, CT ct)
    {
        List<GetProductModel>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            result = await _pRepo.GetProductByIds(ids, ct);
        }
        return result;
    }

    public static async Task<List<Product>?> ProductsDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, IViewProductRepository _pRepo, CT ct)
    {
        List<Product>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            result = await _pRepo.GetFltrProductByIds(ids, filterData, 0, 0, ct);
        }
        return result;
    }

    public static async Task<List<long>?> FilteredProductsDataReceiver(string? filterData, int pageIndex, int pageSize, IMediator _mediator, CT ct)
    {
        List<long>? result = [];
        if (!string.IsNullOrEmpty(filterData))
        {
            var productsData = await _mediator.Send(new GetsFilteredProductsQuery(true, filterData, pageIndex, pageSize), ct);
            result = productsData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<Role>?> RoleDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<Role>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var roleModelData = await _mediator.Send(new GetsRoleByIdQuery(ids!), ct);
            result = roleModelData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<City>?> CityDataReceiver(List<long> ids, IMediator _mediator, CT ct)
    {
        List<City>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var roleModelData = await _mediator.Send(new GetsCityByIdQuery(1, ids.Count, ids, null, true), ct);
            result = roleModelData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<City>?> CityDataReceiver(List<long> ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<City>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var roleModelData = await _mediator.Send(new GetsCityByIdQuery(1, ids.Count, ids, filterData, true), ct);
            result = roleModelData.Value?.Data;
        }
        return result;
    }

    public static async Task<List<Measureunit>?> MeasurementDataReceiver(List<long>? ids, IMediator _mediator, CT ct)
    {
        List<ViewMeasureUnit> entities = [];
        List<Measureunit>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var measureUnitsData = await _mediator.Send(new GetsMeasureunitByIdQuery(1, ids.Count, ids, true), ct);
            if (!measureUnitsData.IsBad() && measureUnitsData.Value!.Data != null)
                foreach (var item in measureUnitsData.Value!.Data)
                    result.Add(new Measureunit(
                        item.Id,
                        item.Name,
                        item.MeasureUnitGroupId,
                        item.ConversionFactor,
                        item.Tolerance,
                        item.IsActive,
                        item.IsPrimary));
        }
        return result;
    }

    public static async Task<List<WarehouseCategory>?> CategoriesDataReceiver(List<long>? ids, string? filterData, IMediator _mediator, CT ct)
    {
        List<WarehouseCategory>? result = [];
        if (ids is not null && ids.Count > 0)
        {
            var categoryIds = ids.Adapt<List<long?>>();
            var categoryData = await _mediator.Send(new GetsWarehouseCategoryByIdQuery(1, categoryIds.Count, categoryIds, true, filterData), ct);
            result = categoryData.Value?.Data;
        }
        return result;
    }

    public static async Task<WarehouseCategory?> CategoryDataReceiver(long? id, IMediator _mediator, CT ct)
    {
        WarehouseCategory? result = null;
        if (id != null)
        {
            var categoryData = await _mediator.Send(new GetWarehouseCategoryByIdQuery((long)id), ct);
            result = categoryData.Value;
        }
        return result;
    }

    public static async Task<List<FilteredGroupsModel>?> GetFilteredGroupsByCategoryIds(List<long> categoryIds, List<long> groupIds, string? filterData, IMediator _mediator, CT ct)
    {
        List<FilteredGroupsModel>? result = null;
        if (categoryIds != null)
        {
            var groupsData = await _mediator.Send(new GetFilteredGroupsByCategoryIdsQuery(categoryIds, null, groupIds, 1, 1000, null, null, filterData, null, null, null), ct);
            result = groupsData.Value?.Data;
        }
        return result;
    }

}
