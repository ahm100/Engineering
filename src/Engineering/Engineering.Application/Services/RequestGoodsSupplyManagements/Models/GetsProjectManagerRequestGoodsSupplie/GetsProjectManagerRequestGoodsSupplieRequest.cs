using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyManagements.Models.GetsProjectManagerRequestGoodsSupplie;

public record GetsProjectManagerRequestGoodsSupplieRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long ProjectManagerId,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    string? FilterData,
    DateTime? FromDate,
    DateTime? ToDate,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    string? CustomerInvoiceNumber,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
