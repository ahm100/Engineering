using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetAllGoodsSupplyProductDocument;
using Engineering.Domain.Entities.RequestGoodsSupplies.Documents;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Abstractions.Data.RequestGoodsSupplies.Documents;

public interface IRequestGoodsSupplyDetailDocumentRepository : IBaseRepository<RequestGoodsSupplyDetailDocument>
{
    Task<List<string>> GetGoodsSupplyProductDocuments(
        long id,
        CT ct);

    Task<List<GetAllGoodsSupplyProductDocumentResponseModel>> GetAllGoodsSupplyProductDocument(
    List<long>? ids,
    List<long>? costCenterIds,
    List<long>? projectIds,
    List<long>? projectOperationIds,
    List<long>? projectOperationDetailIds,
    List<long>? productIds,
    long? cityId,
    List<GoodsSupplyType>? types,
    List<GoodsSupplyDetailStatus>? statuses,
    DateTime? startDate,
    DateTime? endDate,
    int pageIndex,
    int pageSize, CT ct);

    Task<List<RequestGoodsSupplyDetailDocument>?> GetRequestGoodsSupplyProductByIds(
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
    CT ct);
}
