namespace Engineering.Application.Services.ContractorContracts.Contracts.GetServiceFilteredSuggestedPriceHistories;

public record GetServiceFilteredSuggestedPriceHistoriesModel
{
    public List<GetServiceFilteredSuggestedPriceHistoriesDetailModel> Data { get; set; } = new();
    public int RowCount { get; set; }
};