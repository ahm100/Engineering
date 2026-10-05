namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsProductDetailByRequestId;

public class GetsProductDetailByRequestIdModel
{
    public long ProductId { get; set; }
    public string? ProductName { get; set; } = string.Empty;
    public string? ProductCode { get; set; } = string.Empty;
    public float? TotalRequestedCount { get; set; }
    public float? TotalSupplyCount { get; set; }
    public float? TotalRemainedCount { get; set; }
    public List<ProductProjectOperationDetailResponseModel>? ProjectOperationDetails { get; set; }
}
public record ProductProjectOperationDetailResponseModel
{
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public float? RequestedCount { get; set; }
    public float? SupplyCount { get; set; }
    public float? RemainedCount => RequestedCount - SupplyCount;
}