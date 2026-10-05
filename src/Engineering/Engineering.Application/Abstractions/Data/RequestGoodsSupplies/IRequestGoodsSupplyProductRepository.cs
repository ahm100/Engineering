using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetRequestGoodsSupplyProductById;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProduct;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;
using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies;

public interface IRequestGoodsSupplyProductRepository : IBaseRepository<RequestGoodsSupplyProduct>
{
    Task<RequestGoodsSupplyProduct?> GetRequestGoodsSupplyProductById(
        long id,
        CT ct);
    Task<RequestGoodsSupplyProduct?> GetSupplyProductByCommercialCommercialRequestId(
        long commercialRequestId,
        CT ct);

    Task<List<RequestGoodsSupplyProduct>?> GetsRequestGoodsSupplyProductByContractorId(
        DateTime startDate,
        DateTime endDate,
        GoodsSupplyType? type,
        long contractorId,
        long projectId,
        CT ct);

    Task<List<long>?> GetRGPForManagerProductIds(
        CT ct);

    Task<RequestGoodsSupplyProduct?> GetRequestGoodsSupplyProductForChangeStatus(
        long id,
        CT ct);

    Task<GetRequestGoodsSupplyProductByIdResponse?> GetRequestGoodsSupplyProductByIdModeled(
        long id,
        CT ct);

    Task<GetGoodsSupplyProductByIdResponse?> GetGoodsSupplyProductById(
        long id,
        CT ct);

    Task<(List<GetsRequestGoodsSupplyProductModel> Data, int RowCount)> GetsRequestGoodsSupplyProduct(
        List<long>? ids,
        List<long>? requestGoodsSupplyIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        long? thirdPartyId,
        bool chechThirdParty,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string[]? orderBy,
        bool isExcel,
        bool containDraft,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsGoodsSupplyProductModel> Data, int RowCount)> GetsGoodsSupplyProduct(
        List<long>? ids,
        List<long>? requestGoodsSupplyIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        List<long>? contractorIds,
        List<long>? buyerIds,
        List<long>? supplyerIds,
        List<long>? managerSelectedProductIds,
        long? thirdPartyId,
        long? cityId,
        long? rGSId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        bool isDraft,
        string[]? orderBy,
        bool isExcel,
        long companyId,
        bool checkThirdParty,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<decimal> GetsTotalPriceRequestGoodsSupplyProduct(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        DateTime? startDate,
        DateTime? endDate,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        CT ct);

    Task<(List<RequestGoodsSupplyProduct> Data, int RowCount)> GetsRequestGoodsSupplyProductByIds(
        List<long>? ids,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<(List<GetsGoodsSupplyDetailBySupplyProductIdWithScaleModel> Data, int RowCount)> GetsGoodsSupplyDetailBySupplyProductIdWithScale(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? productIds,
        List<long>? creatorIds,
        List<long>? warehouseIds,
        long? cityId,
        long? projectManagerId,
        List<GoodsSupplyDetailImportance>? importances,
        List<GoodsSupplyType>? types,
        List<GoodsSupplyDetailStatus>? statuses,
        List<GoodsSupplyDetailStatus>? removeStatuses,
        string? requestNumber,
        string? filterDescription,
        string? filterPublicName,
        string? filterOperationInfoName,
        string? filterManagerDescription,
        string? filterData,
        string? customerInvoiceNumber,
        string? filterProduct,
        DateTime? fromDate,
        DateTime? toDate,
        int pageIndex,
        int pageSize,
       CT ct);

    Task<long> GetProjectManagerId(
        long id, CT ct);
}
