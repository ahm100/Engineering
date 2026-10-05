namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyFiltered;

public record GetFilteredCurrenciesResponse
{
    [JsonProperty("value")]
    public GetFilteredCurrenciesModel? Value { get; set; }
}