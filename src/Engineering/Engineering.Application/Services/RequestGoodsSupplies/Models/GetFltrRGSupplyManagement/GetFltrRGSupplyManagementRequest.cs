using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetFltrRGSupplyManagement;

public record GetFltrRGSupplyManagementRequest(
        List<long>? Ids,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? ProductGroupIds,
        List<long>? ProductIds,
        List<long>? CreatorIds,
        List<long>? ManagementIds,
        List<GoodsSupplyDetailStatus>? Statuses,
        List<GoodsSupplyDetailStatus>? RemoveStatuses,
        DateTime? StartDate,
        DateTime? EndDate,
        long? ProjectManagerId,
        long? ThirdPartyId,
        long? CityId,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize
    ) : IHttpRequest;
