namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetProjectOperationDetailsByRequestId;

public class GetProjectOperationDetailsByRequestIdModel
{
    public long ProjectOperationDetailId { get; set; }
    public string? PrivateName { get; set; } = string.Empty;
    public string? PrivateCode { get; set; } = string.Empty;
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public decimal? EstimatedCount { get; set; } = 0;
    public decimal? ProvidedCount { get; set; } = 0;
    public decimal? RequestedCount { get; set; } = 0;
}
