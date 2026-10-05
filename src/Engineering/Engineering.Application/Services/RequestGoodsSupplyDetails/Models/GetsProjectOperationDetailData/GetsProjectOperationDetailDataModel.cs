namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProjectOperationDetailData;

public class GetsProjectOperationDetailDataModel
{
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public float? TolerancePercentage { get; set; } = 0;
    public float? ToleranceCount { get; set; } = 0;
    public float? TotalEstimatedCount { get; set; } = 0;
    public float? TotalRequestedCount { get; set; } = 0;
    public float? TotalSupplyCount { get; set; } = 0;
    public float? TotalRemainedCount { get; set; } = 0;
    public string? Description { get; set; } = string.Empty;
}
