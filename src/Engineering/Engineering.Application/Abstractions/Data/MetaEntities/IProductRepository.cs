using Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrProducts;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProducts;
using Engineering.Domain.Entities.Synonyms.Warehouse.Products;

namespace Engineering.Application.Abstractions.Data.MetaEntities;

public interface IViewProductRepository : IBaseRepository<ViewProduct>
{
    Task<ViewProduct?> GetProductById(
          long id,
          CT ct);

    Task<List<ViewProduct>?> GetProductByGroupIds(
        List<long> ids,
        string? productFilterData,
        CT ct);

    Task<List<ViewProduct>?> GetProductByCodes(
        List<string> codes,
        CT ct);

    Task<(List<ViewProduct> Data, int RowCount)> GetProductsByIds(
           List<long> ids,
           bool? ignoreQuery,
           string? filterData,
           int pageIndex,
           int pageSize,
           CT ct);

    Task<List<GetGoodsSupplyDetailProductsModelDetail>?> GetProductForDetailsModel(
    List<long> ids,
    string? productFilterData,
    CT ct);

    Task<List<GetGoodsSupplyDetailProductsModelDetail>?> GetProductForPOProductsDetailsModel(
    List<long> ids,
    string? productFilterData,
    CT ct);

    Task<List<GetProductModel>?> GetProductByIds(
        List<long> ids,
        CT ct);

    Task<List<Product>?> GetFltrProductByIds(
        List<long> ids,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetFltrProductsModel>? Data, int RowCount)> GetFltrProducts(
        List<long>? catIds,
        List<long>? groupIds,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);
}