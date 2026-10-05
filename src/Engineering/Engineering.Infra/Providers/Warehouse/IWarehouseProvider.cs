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

public interface IWarehouseProvider
{

    //  دریافت گروه های کالا
    [Delete("/invoice/v1/remove")]
    Task<RemoveInvoiceResponse> RemoveInvoice(
        [AliasAs("Id")] long Id, CT ct);

    //  دریافت گروه های کالا
    [Post("/inventory/v1/getProductInventoryByFilter")]
    Task<GetProductInventoryByFilterResponse> GetProductInventoryByFilter(
        [Body] GetProductInventoryByFilterRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/product/v1/getByIds")]
    Task<GetsFilteredProductByIdsResponse> GetsFilteredProductByIds(
        [Body] GetsFilteredProductByIdsRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/product/v1/getProductsByFilter")]
    Task<GetsFilteredProductsResponse> GetsFilteredProducts(
        [Body] GetsFilteredProductsRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/group/v1/getFilteredGroupsByIds")]
    Task<GetFilteredGroupsByIdsResponse> GetFilteredGroupsByIds(
        [Body] GetFilteredGroupsByIdsRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/warehouse/v1/getFilteredWarehousesByGroupId")]
    Task<GetFilteredWarehousesByGroupIdResponse> GetFilteredWarehousesByGroupId(
        [Body] GetFilteredWarehousesByGroupIdRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/package/v1/getByProductId")]
    Task<GetPackageByProductIdResponse> GetPackageByProductId(
        [Body] GetPackageByProductIdRequest request, CT ct);

    //  دریافت گروه های کالا
    [Post("/warehouse/v1/getFilteredByGroupIds")]
    Task<GetFilteredWarehousesByGroupIdsResponse> GetFilteredWarehousesByGroupIds(
        [Body] GetFilteredWarehousesByGroupIdsRequest request, CT ct);

    [Post("/invoice/v1/createExitForConsume")]
    Task<Result<CreateInvoiceResponse?>> CreateExitForConsumes(
        [Body] CreateExitForConsumesRequest request, CT ct);

    [Post("/invoice/v1/createExitForRelocation")]
    Task<Result<CreateInvoiceResponse?>> CreateExitForRelocations(
        [Body] CreateExitForRelocationsRequest request, CT ct);

    [Post("/invoice/v1/CreateEntryThroughBorrow")]
    Task<Result<CreateInvoiceResponse?>> CreateEntryThroughBorrow(
        [Body] CreateEntryThroughBorrowRequest request, CT ct);

    [Post("/invoice/v1/CreateExitForBorrow")]
    Task<Result<CreateInvoiceResponse?>> CreateExitForBorrow(
        [Body] CreateExitForBorrowRequest request, CT ct);

    // دریافت اطلاعات واحد اندازه گیری 
    [Get("/invoiceproduct/v1/getByInvoiceId")]
    Task<GetAllActiveInvoiceProductsResponse> GetByInvoiceId(
        [AliasAs("InvoiceId")] long invoiceId, [AliasAs("Name")] string? name, [AliasAs("Code")] string? code,
        [AliasAs("filterData")] string? FilterData, [AliasAs("brandId")] long? BrandId, [AliasAs("brandModelId")] long? BrandModelId,
        [AliasAs("PageIndex")] int pageIndex, [AliasAs("PageSize")] int pageSize, CT ct);

    // دریافت اطلاعات دسته بندی 
    [Get("/category/v1/getById")]
    Task<GetWarehouseCategoryByIdResponse> GetWarehouseCategoryById(
        [AliasAs("Id")] long Id, CT ct);

    // دریافت اطلاعات دسته بندی 
    [Get("/warehouse/v1/GetsMainWarehouseIds")]
    Task<GetsMainWarehouseIdsResponse> GetsMainWarehouseIds(
        CT ct);

    // دریافت اطلاعات دسته بندیها 
    [Post("/category/v1/getCategoryByIds")]
    Task<GetsWarehouseCategoryByIdResponse> GetsWarehouseCategoryById(
        [Body] GetsWarehouseCategoryByIdRequest request, CT ct);

    // دریافت اطلاعات دسته بندیها 
    [Post("/group/v1/getByCategoryIds")]
    Task<GetFilteredGroupsByCategoryIdsResponse> GetFilteredGroupsByCategoryIds(
        [Body] GetFilteredGroupsByCategoryIdsRequest request, CT ct);

    [Post("/group/v1/getGroupsForGoodsSupplyByCategoryIds")]
    Task<GetGroupsForGoodsProductResponse> GetGroupsForGoodsSupplyByCategoryIds(
        [Body] GetGroupsForGoodsProductRequest request, CT ct);

    // دریافت اطلاعات دسته بندیها 
    [Get("/group/v1/getActives")]
    Task<GetActiveGroupsResponse> GetActiveGroups(
        [AliasAs("FilterData")] string? filterData, [AliasAs("PageIndex")] int pageIndex, [AliasAs("PageSize")] int pageSize, CT ct);

    [Post("/group/v1/getGroupsByCategoryIdsAndWarehouseIds")]
    Task<GetProductGroupByWareHouseIdResponse> GetProductGroupByWareHouseId(
        [Body] GetProductGroupByWareHouseIdRequest request, CT ct);

    [Post("/product/v1/getByGroupIds")]
    Task<GetProductByGroupIdsResponse> GetProductByGroupIds(
        [Body] GetProductByGroupIdsRequest request, CT ct);

    [Post("/group/v1/getByCategoryIds")]
    Task<GetByCategoryIdsResponse> GetByCategoryIds(
        [Body] GetGroupByCategoryIdsRequest request, CT ct);

    [Post("/warehouseasset/v1/GetUnUsedWarehousesCategories")]
    Task<GetUnUsedWarehousesCategoriesResponse> GetUnUsedWarehousesCategories(
        [Body] GetUnUsedWarehousesCategoriesQuery request, CT ct);

    [Post("/warehouseasset/v1/GetUnUsedWarehousesGroups")]
    Task<GetUnUsedWarehousesGroupsResponse> GetUnUsedWarehousesGroups(
        [Body] GetUnUsedWarehousesGroupsQuery request, CT ct);

    [Post("/warehouse/v1/getByIds")]
    Task<GetWarehouseByIdsResponse> GetWarehouseByIds(
        [Body] GetWarehouseByIdsRequest request, CT ct);

}