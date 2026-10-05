using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.RequestGoodsSupplyManagements.Models.GetFilteredManagementRequestGoodsSupplies;

public record GetFilteredManagementRequestGoodsSuppliesRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? ProjectManagerId,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    string? FilterData,
    DateTime? FromDate,
    DateTime? ToDate,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    List<GoodsSupplyType>? RemoveTypes,
    string? CustomerInvoiceNumber,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
