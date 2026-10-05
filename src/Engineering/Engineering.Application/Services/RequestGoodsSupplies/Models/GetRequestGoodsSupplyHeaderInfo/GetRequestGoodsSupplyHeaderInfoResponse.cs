namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetRequestGoodsSupplyHeaderInfo;

public class GetRequestGoodsSupplyHeaderInfoResponse
{
    public long CostCenterId { get; set; }
    public string CostCenterName { get; set; } = string.Empty;
    public long ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public long ProjectOperationId { get; set; }
    public string OperationInfoCode { get; set; } = string.Empty;
    public string OperationInfoName { get; set; } = string.Empty;
    public long? MeasureId { get; set; }
    public string? MeasureName { get; set; } = string.Empty;
    public decimal Workload { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal? FinalAmount { get; set; }
}
