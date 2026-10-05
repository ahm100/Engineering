using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyDetailBySupplyProductId;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetTotalSupplyByProductIds;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyDetailRepository : IBaseRepository<RequestGoodsSupplyDetail>
{
    Task<RequestGoodsSupplyDetail?> GetRequestGoodsSupplyDetailById(long id, CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetail(long id, List<GoodsSupplyDetailStatus>? statuses, List<GoodsSupplyDetailStatus>? removeStatuses,
         string[]? orderBy, int pageIndex, int pageSize, CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsGoodsSupplyDetailByProductId(long id, int pageIndex, int pageSize, CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsGoodsSupplyDetailByProjectOperationId(long projectOperationId, int pageIndex, int pageSize, CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> RequestGoodsSupplyDetailsByRequestIdAsync(long requestId, CT ct);
    Task<RequestGoodsSupplyDetail> GetRequestGoodSupply(
        long goodsSupplyProductId, CT ct);
    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetailByIds(List<long> ids, CT ct);
    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetFilteredAlternativeProducts(List<long>? requestIds, List<long>? products, List<long>? requestGoodsSupplyDetailIds, CT ct);
    Task<List<GetTotalSupplyByProductIdsModel>> GetTotalSupplyByProductIdsAsync(List<long>? products, CT ct);
    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsRequestGoodsSupplyDetailForDaily(long projectOperationDetailId, CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetsProductDetailByRequestId(
       long requestGoodsSupplyId,
       CT ct);

    Task<List<GetsGoodsSupplyDetailBySupplyProductIdModel>> GetsGoodsSupplyDetailBySupplyProductId(
       long goodsSupplyProductId,
       CT ct);

    Task<List<GetsGoodsSupplyDetailBySupplyProductIdModel>> GetsProjectGoodsSupplyDetailBySupplyProductId(
        long goodsSupplyProductId,
        CT ct);

    Task<(List<RequestGoodsSupplyDetail> Data, int RowCount)> GetRequestGoodsSupplyDetailsForExcel(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyStatus>? statuses,
        List<GoodsSupplyStatus>? removeStatuses,
        List<GoodsSupplyType>? types,
        List<long>? creatorIds,
        List<long>? productIds,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<decimal> GetDailyRequestProductsCount(
        CT ct);

    Task<decimal> GetDailyRequestProvidedProductsCount(
        CT ct);
}
