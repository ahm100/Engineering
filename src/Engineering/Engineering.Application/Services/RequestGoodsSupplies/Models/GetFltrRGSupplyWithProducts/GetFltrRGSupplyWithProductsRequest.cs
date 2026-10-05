using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSupplyForManagement;

public class GetFltrRGSupplyWithProductsRequest : IHttpRequest
{
    public List<long>? Ids { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public List<long>? ProductGroupIds { get; set; }
    public List<long>? ProductIds { get; set; }
    public List<long>? CreatorIds { get; set; }
    public List<long>? ManagementIds { get; set; }
    public List<GoodsSupplyType>? Types { get; set; }
    public List<GoodsSupplyDetailStatus>? Statuses { get; set; }
    public List<GoodsSupplyDetailStatus>? RemoveStatuses { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public long? ProjectManagerId { get; set; }
    public long? ThirdPartyId { get; set; }
    public long? CityId { get; set; }
    public string? FilterData { get; set; }
    public string[]? OrderBy { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
