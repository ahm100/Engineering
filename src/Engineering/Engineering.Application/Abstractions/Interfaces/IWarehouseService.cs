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

namespace Engineering.Application.Abstractions.Interfaces;

public interface IWarehouseService
{
    // دریافت کالا
    Task<RemoveInvoiceResponse?> RemoveInvoice(RemoveInvoiceRequest request, CT ct);

    // دریافت کالا
    Task<GetProductInventoryByFilterResponse?> GetProductInventoryByFilter(GetProductInventoryByFilterRequest request, CT ct);

    // دریافت کالا
    Task<GetsMainWarehouseIdsResponse?> GetsMainWarehouseIds(CT ct);

    // دریافت کالا
    Task<GetsFilteredProductByIdsResponse?> GetsFilteredProductByIds(GetsFilteredProductByIdsRequest request, CT ct);

    // دریافت کالا
    Task<GetsFilteredProductsResponse?> GetsFilteredProducts(GetsFilteredProductsRequest request, CT ct);

    //دریافت گروه کالا ها
    Task<GetFilteredGroupsByIdsResponse> GetFilteredGroupsByIds(GetFilteredGroupsByIdsRequest request, CT ct);

    Task<Result<CreateInvoiceResponse?>> CreateExitForConsume(CreateExitForConsumesRequest request, CT ct);

    Task<Result<CreateInvoiceResponse?>> CreateExitForRelocation(CreateExitForRelocationsRequest request, CT ct);

    Task<Result<CreateInvoiceResponse?>> CreateEntryThroughBorrow(CreateEntryThroughBorrowRequest request, CT ct);

    Task<Result<CreateInvoiceResponse?>> CreateExitForBorrow(CreateExitForBorrowRequest request, CT ct);

    Task<GetAllActiveInvoiceProductsResponse?> GetByInvoiceId(GetAllActiveInvoiceProductsRequest request, CT ct);
    Task<GetFilteredWarehousesByGroupIdResponse?> GetFilteredWarehousesByGroupId(GetFilteredWarehousesByGroupIdRequest request, CT ct);
    Task<GetPackageByProductIdResponse?> GetPackageByProductId(GetPackageByProductIdRequest request, CT ct);
    // دریافت دسته بندی
    Task<GetWarehouseCategoryByIdResponse?> GetWarehouseCategoryById(GetWarehouseCategoryByIdRequest request, CT ct);

    // دریافت دسته بندی ها
    Task<GetsWarehouseCategoryByIdResponse?> GetsWarehouseCategoryById(GetsWarehouseCategoryByIdRequest request, CT ct);

    // دریافت گرو های دستبندی
    Task<GetFilteredGroupsByCategoryIdsResponse?> GetFilteredGroupsByCategoryIds(GetFilteredGroupsByCategoryIdsRequest request, CT ct);
    Task<GetGroupsForGoodsProductResponse?> GetGroupsForGoodsSupplyByCategoryIds(GetGroupsForGoodsProductRequest request, CT ct);
    Task<GetFilteredWarehousesByGroupIdsResponse?> GetFilteredWarehousesByGroupIds(GetFilteredWarehousesByGroupIdsRequest request, CT ct);
    Task<GetActiveGroupsResponse?> GetActiveGroups(GetActiveGroupsRequest request, CT ct);
    Task<GetProductGroupByWareHouseIdResponse?> GetProductGroupByWareHouseId(GetProductGroupByWareHouseIdRequest request, CT ct);
    Task<GetProductByGroupIdsResponse?> GetProductByGroupIds(GetProductByGroupIdsRequest request, CT ct);
    Task<GetByCategoryIdsResponse?> GetByCategoryIds(GetGroupByCategoryIdsRequest request, CT ct);
    Task<GetUnUsedWarehousesCategoriesResponse?> GetUnUsedWarehousesCategories(GetUnUsedWarehousesCategoriesQuery request, CT ct);
    Task<GetUnUsedWarehousesGroupsResponse?> GetUnUsedWarehousesGroups(GetUnUsedWarehousesGroupsQuery request, CT ct);
    Task<GetWarehouseByIdsResponse?> GetWarehouseByIds(GetWarehouseByIdsRequest request, CT ct);
}