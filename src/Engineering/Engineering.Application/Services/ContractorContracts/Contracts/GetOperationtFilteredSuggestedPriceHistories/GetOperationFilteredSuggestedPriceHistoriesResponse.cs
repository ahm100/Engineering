namespace Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;

public record GetOperationFilteredSuggestedPriceHistoriesResponse
{
    public long ProjectOperationId { get; set; }
    public string OperationInfoName { get; set; } = string.Empty;
    public string OperationInfoCode { get; set; } = string.Empty;
    public decimal WorkLoad { get; set; }
    public long? MeasureUnitId { get; set; }
    public string? MeasureUnit { get; set; } = string.Empty;
    public GetOperationFilteredSuggestedPriceHistoriesModel Detail { get; set; } = new();
}
