using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProduct;

public class GetsRequestGoodsSupplyProductRequest : IHttpRequest
{
    public List<long>? Ids { get; set; }
    public List<long>? requestGoodsSupplyIds { get; set; }
    public List<long>? CostCenterIds { get; set; }
    public List<long>? ProjectIds { get; set; }
    public List<long>? ProjectOperationIds { get; set; }
    public List<long>? ProjectOperationDetailIds { get; set; }
    public List<long>? ProductIds { get; set; }
    public List<long>? CreatorIds { get; set; }
    public List<long>? WarehouseIds { get; set; }
    public long? CityId { get; set; }
    public long? ProjectManagerId { get; set; }
    public long? ThirdPartyId { get; set; }
    public List<GoodsSupplyDetailImportance>? Importances { get; set; }
    public List<GoodsSupplyType>? Types { get; set; }
    public List<GoodsSupplyDetailStatus>? Statuses { get; set; }
    public List<GoodsSupplyDetailStatus>? RemoveStatuses { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? RequestNumber { get; set; }
    public string? FilterDescription { get; set; }
    public string? FilterPublicName { get; set; }
    public string? FilterOperationInfoName { get; set; }
    public string? FilterManagerDescription { get; set; }
    public string? FilterData { get; set; }
    public string? CustomerInvoiceNumber { get; set; }
    public string? FilterProduct { get; set; }
    public string[]? OrderBy { get; set; }
    public bool? IsExcel { get; set; }
    public bool ContainDraft { get; set; } = false;
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}
