using Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsRequestGoodsSupplyProduct;

public record GetsRequestGoodsSupplyProductQuery(
    List<long>? Ids,
    List<long>? requestGoodsSupplyIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ProductIds,
    List<long>? CreatorIds,
    List<long>? WarehouseIds,
    long? CityId,
    long? ProjectManagerId,
    long? ThirdPartyId,
    List<GoodsSupplyDetailImportance>? Importances,
    List<GoodsSupplyType>? Types,
    List<GoodsSupplyDetailStatus>? Statuses,
    List<GoodsSupplyDetailStatus>? RemoveStatuses,
    DateTime? StartDate,
    DateTime? EndDate,
    string? RequestNumber,
    string? FilterDescription,
    string? FilterPublicName,
    string? FilterOperationInfoName,
    string? FilterManagerDescription,
    string? FilterData,
    string? CustomerInvoiceNumber,
    string[]? OrderBy,
    bool IsExcel,
    bool ContainDraft,
    bool ChechThirdParty,
    int PageIndex,
    int PageSize
    ) : IQuery<DataResult<List<GetsRequestGoodsSupplyProductModel>>>;
