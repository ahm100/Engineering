using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetActiveGroups;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByCategoryIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredGroupsByIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupId;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetFilteredWarehousesByGroupIds;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetGroupsForGoodsProduct;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetProductGroupByWareHouseId;
using Engineering.Application.WebServices.WarehouseServices.Groups.Models.GetsMainWarehouseIds;
using Engineering.Application.WebServices.WarehouseServices.Inventories.Models.GetProductInventoryByFilter;
using Engineering.Application.WebServices.WarehouseServices.InvoiceProducts.Models.GetByInvoiceId;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateEntryThroughBorrow;
using Engineering.Application.WebServices.WarehouseServices.Invoices.Models.CreateExitForBorrows;
using Engineering.Application.WebServices.WarehouseServices.Packages.Models.GetPackageByProductId;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForConsumes;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.CreateExitForRelocations;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetProductByGroupIds;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProductByIds;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.GetsFilteredProducts;
using Engineering.Application.WebServices.WarehouseServices.Products.Models.RemoveInvoice;
using Engineering.Application.WebServices.WarehouseServices.Warehouse.Models.GetWarehouseByIds;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesCategories;
using Engineering.Application.WebServices.WarehouseServices.WarehouseAssetes.Queries.GetUnUsedWarehousesGroups;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetsWarehouseCategoryById;
using Engineering.Application.WebServices.WarehouseServices.WarehouseCategories.Models.GetWarehouseCategoryById;

namespace Engineering.Infra.Providers.Warehouse;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseProvider _warehouseProvider;

    public WarehouseService(IWarehouseProvider warehouseProvider)
    {
        _warehouseProvider = warehouseProvider;
    }


    public async Task<GetProductInventoryByFilterResponse?> GetProductInventoryByFilter(GetProductInventoryByFilterRequest request, CT ct)
    {
        return await _warehouseProvider.GetProductInventoryByFilter(request, ct);
    }

    public async Task<GetsMainWarehouseIdsResponse?> GetsMainWarehouseIds(CT ct)
    {
        return await _warehouseProvider.GetsMainWarehouseIds(ct);
    }

    public async Task<RemoveInvoiceResponse?> RemoveInvoice(RemoveInvoiceRequest request, CT ct)
    {
        return await _warehouseProvider.RemoveInvoice(request.Id, ct);
    }

    public async Task<GetsFilteredProductByIdsResponse?> GetsFilteredProductByIds(GetsFilteredProductByIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetsFilteredProductByIds(request, ct);
    }


    public async Task<GetsFilteredProductsResponse?> GetsFilteredProducts(GetsFilteredProductsRequest request, CT ct)
    {
        return await _warehouseProvider.GetsFilteredProducts(request, ct);
    }

    public async Task<Result<CreateInvoiceResponse?>> CreateExitForConsume(CreateExitForConsumesRequest request, CT ct)
    {
        return await _warehouseProvider.CreateExitForConsumes(request, ct);
    }

    public async Task<Result<CreateInvoiceResponse?>> CreateExitForRelocation(CreateExitForRelocationsRequest request, CT ct)
    {
        return await _warehouseProvider.CreateExitForRelocations(request, ct);
    }

    public async Task<Result<CreateInvoiceResponse?>> CreateEntryThroughBorrow(CreateEntryThroughBorrowRequest request, CT ct)
    {
        return await _warehouseProvider.CreateEntryThroughBorrow(request, ct);
    }

    public async Task<Result<CreateInvoiceResponse?>> CreateExitForBorrow(CreateExitForBorrowRequest request, CT ct)
    {
        return await _warehouseProvider.CreateExitForBorrow(request, ct);
    }

    public async Task<GetFilteredGroupsByIdsResponse> GetFilteredGroupsByIds(GetFilteredGroupsByIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetFilteredGroupsByIds(request, ct);
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<GetFilteredWarehousesByGroupIdResponse> GetFilteredWarehousesByGroupId(GetFilteredWarehousesByGroupIdRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        return await _warehouseProvider.GetFilteredWarehousesByGroupId(request, ct);
    }


#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<GetPackageByProductIdResponse> GetPackageByProductId(GetPackageByProductIdRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        return await _warehouseProvider.GetPackageByProductId(request, ct);
    }


#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<GetFilteredWarehousesByGroupIdsResponse> GetFilteredWarehousesByGroupIds(GetFilteredWarehousesByGroupIdsRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        return await _warehouseProvider.GetFilteredWarehousesByGroupIds(request, ct);
    }

#pragma warning disable CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    public async Task<GetActiveGroupsResponse> GetActiveGroups(GetActiveGroupsRequest request, CT ct)
#pragma warning restore CS8613 // Nullability of reference types in return type doesn't match implicitly implemented member.
    {
        return await _warehouseProvider.GetActiveGroups(request.FilterData, request.PageIndex, request.PageSize, ct);
    }

    public async Task<GetAllActiveInvoiceProductsResponse?> GetByInvoiceId(GetAllActiveInvoiceProductsRequest request, CT ct)
    {
        return await _warehouseProvider.GetByInvoiceId(request.InvoiceId, request.Name, request.Code, request.FilterData, request.BrandId, request.BrandModelId, request.PageIndex, request.PageSize, ct);
    }

    public async Task<GetWarehouseCategoryByIdResponse?> GetWarehouseCategoryById(GetWarehouseCategoryByIdRequest request, CT ct)
    {
        return await _warehouseProvider.GetWarehouseCategoryById(request.Id, ct);
    }

    public async Task<GetsWarehouseCategoryByIdResponse?> GetsWarehouseCategoryById(GetsWarehouseCategoryByIdRequest request, CT ct)
    {
        return await _warehouseProvider.GetsWarehouseCategoryById(request, ct);
    }

    public async Task<GetFilteredGroupsByCategoryIdsResponse?> GetFilteredGroupsByCategoryIds(GetFilteredGroupsByCategoryIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetFilteredGroupsByCategoryIds(request, ct);
    }

    public async Task<GetGroupsForGoodsProductResponse?> GetGroupsForGoodsSupplyByCategoryIds(GetGroupsForGoodsProductRequest request, CT ct)
    {
        return await _warehouseProvider.GetGroupsForGoodsSupplyByCategoryIds(request, ct);
    }

    public async Task<GetProductGroupByWareHouseIdResponse?> GetProductGroupByWareHouseId(GetProductGroupByWareHouseIdRequest request, CT ct)
    {
        return await _warehouseProvider.GetProductGroupByWareHouseId(request, ct);
    }

    public async Task<GetProductByGroupIdsResponse?> GetProductByGroupIds(GetProductByGroupIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetProductByGroupIds(request, ct);
    }

    public async Task<GetByCategoryIdsResponse?> GetByCategoryIds(GetGroupByCategoryIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetByCategoryIds(request, ct);
    }

    public async Task<GetUnUsedWarehousesCategoriesResponse?> GetUnUsedWarehousesCategories(GetUnUsedWarehousesCategoriesQuery request, CT ct)
    {
        return await _warehouseProvider.GetUnUsedWarehousesCategories(request, ct);
    }

    public async Task<GetUnUsedWarehousesGroupsResponse?> GetUnUsedWarehousesGroups(GetUnUsedWarehousesGroupsQuery request, CT ct)
    {
        return await _warehouseProvider.GetUnUsedWarehousesGroups(request, ct);
    }

    public async Task<GetWarehouseByIdsResponse?> GetWarehouseByIds(GetWarehouseByIdsRequest request, CT ct)
    {
        return await _warehouseProvider.GetWarehouseByIds(request, ct);
    }
}