namespace Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;

public record GetOperationFilteredSuggestedPriceHistoriesDetailModel
{
    public string? CostCenterName { get; set; } = string.Empty;
    public string? ProjectName { get; set; } = string.Empty;
    public string? RequestNumber { get; set; } = string.Empty;
    public string? ContractorName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal WorkLoad { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Price { get; set; }
    public string? Currency { get; set; } = string.Empty;
}
