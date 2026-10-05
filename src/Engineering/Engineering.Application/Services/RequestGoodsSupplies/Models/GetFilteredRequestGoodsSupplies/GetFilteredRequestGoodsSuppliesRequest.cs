using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFilteredRequestGoodsSupplies;

public record GetFilteredRequestGoodsSuppliesRequest(
    List<long>? CostCenterIds,
    List<long>? ProjectIds,
    List<long>? ProjectOperationIds,
    List<long>? ProjectOperationDetailIds,
    long? CityId,
    long? ProjectManagerId,
    List<GoodsSupplyStatus>? Statuses,
    List<GoodsSupplyStatus>? RemoveStatuses,
    List<GoodsSupplyType>? Types,
    List<long>? CreatorIds,
    List<long>? ProductIds,
    DateTime? FromDate,
    DateTime? ToDate,
    string? FilterProduct,
    string? FilterDescription,
    string? FilterPublicName,
    string? FilterOperationInfoName,
    string? FilterData,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
    ) : IHttpRequest;
