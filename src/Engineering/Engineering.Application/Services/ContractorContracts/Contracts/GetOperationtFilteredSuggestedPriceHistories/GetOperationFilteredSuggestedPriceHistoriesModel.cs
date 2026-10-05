namespace Engineering.Application.Services.ContractorContracts.Contracts.GeOperationtFilteredSuggestedPriceHistories;

public record GetOperationFilteredSuggestedPriceHistoriesModel
{
    public List<GetOperationFilteredSuggestedPriceHistoriesDetailModel> Data { get; set; } = new();
    public int RowCount { get; set; }
};