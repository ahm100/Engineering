namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;

public record GetServiceFilteredSuggestedPriceHistoriesResponse
{
    public long? ProjectOperationId { get; set; }
    public string? OperationInfoName { get; set; } = string.Empty;
    public string? OperationInfoCode { get; set; } = string.Empty;
    public decimal? OperationWorkLoad { get; set; }
    public long? OperationMeasureUnitId { get; set; }
    public string? OperationMeasureUnit { get; set; } = string.Empty;
    public string? ServiceInfoCode { get; set; } = string.Empty;
    public string? ServiceInfoName { get; set; } = string.Empty;
    public decimal? ServiceWorkLoad { get; set; }
    public long? ServiceMeasureUnitId { get; set; }
    public string? ServiceMeasureUnit { get; set; } = string.Empty;
    public GetServiceFilteredSuggestedPriceHistoriesModel Detail { get; set; } = new();
}
