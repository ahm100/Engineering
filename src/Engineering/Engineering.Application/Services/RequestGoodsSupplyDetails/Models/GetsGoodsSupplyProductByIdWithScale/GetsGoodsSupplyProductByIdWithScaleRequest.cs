
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsGoodsSupplyProductByIdWithScale;

public record GetsGoodsSupplyProductByIdWithScaleRequest(
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
    string? RequestNumber,
    string? FilterDescription,
    string? FilterPublicName,
    string? FilterOperationInfoName,
    string? FilterManagerDescription,
    string? FilterData,
    string? CustomerInvoiceNumber,
    string? FilterProduct,
    DateTime? FromDate,
    DateTime? ToDate,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
