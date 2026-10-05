namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServicePriceHistory;

public record GetServicePriceHistoryResponse(
    List<GetServicePriceHistoryModel> Data,
    int RowCount);

public class GetServicePriceHistoryModel
{
    public long Id { get; set; }
    public long ServiceInfoId { get; set; }
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? RequestNumber { get; set; } = string.Empty;
    public string? ContractorName { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal? WorkLoad { get; set; }
    public decimal? UnitPrice { get; set; }
    public decimal? TotalPrice { get; set; }
    public string? Currency { get; set; } = string.Empty;
}
