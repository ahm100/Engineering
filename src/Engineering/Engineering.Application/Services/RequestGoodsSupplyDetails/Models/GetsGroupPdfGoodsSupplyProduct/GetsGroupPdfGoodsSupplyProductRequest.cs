using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGroupPdfGoodsSupplyProduct;

public record GetsGroupPdfGoodsSupplyProductRequest(
    List<long>? Ids,
    List<long>? RequestGoodsSupplyIds,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ProductIds,
    List<long>? CreatorIds,
    List<long>? WarehouseIds,
    List<long>? ContractorIds,
    List<long>? BuyerIds,
    List<long>? SupplyerIds,
    long? CityId,
    long? RequestGoodsSupplyId,
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
    bool IsDeraft,
    string? FilterProduct,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;

