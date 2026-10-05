using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupply;

public record GetsRequestGoodsSupplyRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    long? CityId,
    long? ProjectManagerId,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
