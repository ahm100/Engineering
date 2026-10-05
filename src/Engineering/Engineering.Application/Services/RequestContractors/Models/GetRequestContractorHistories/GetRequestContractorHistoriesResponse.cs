namespace Engineering.Application.Services.RequestContractors.Models.GetRequestContractorHistories;

public record GetRequestContractorHistoriesResponse
{
    public long? RequestNumber { get; set; }
    public long? ProjectOperationDetailId { get; set; }
    public string? PublicName { get; set; } = string.Empty;
    public string? PublicCode { get; set; } = string.Empty;
    public long? ServiceInfoId { get; set; }
    public string? ServiceInfoName { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public decimal? Volume { get; set; }
    public List<GetRequestContractorHistoriesModel>? Data { get; set; }
    public int RowCount { get; set; }
}
