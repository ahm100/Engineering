using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Queries.GetsTotalPriceRequestGoodsSupplyProduct;

public record GetsTotalPriceRequestGoodsSupplyProductQuery(
    List<long>? Ids,
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    List<long>? ProductIds,
    List<long>? CreatorIds,
    List<long>? WarehouseIds,
    long? CityId,
    long? ProjectManagerId,
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
    string? CustomerInvoiceNumber
    ) : IQuery<decimal>;
