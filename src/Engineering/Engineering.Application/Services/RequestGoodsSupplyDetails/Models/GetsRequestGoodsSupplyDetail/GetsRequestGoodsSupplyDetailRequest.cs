using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyDetail;

public record GetsRequestGoodsSupplyDetailRequest(
    long Id,
    //string? FilterData,
    List<GoodsSupplyDetailStatus>? Statuses,
    List<GoodsSupplyDetailStatus>? RemoveStatuses,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
