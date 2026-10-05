using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetsCurrencyFiltered;

public record GetFilteredCurrenciesModel
{
    [JsonProperty("data")]
    public List<CurrencyModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}
