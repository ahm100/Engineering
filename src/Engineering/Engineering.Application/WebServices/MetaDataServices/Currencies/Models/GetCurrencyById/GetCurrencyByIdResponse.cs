using CurrencyModel = Engineering.Application.WebServices.MetaDataServices.Currencies.Models.Currency;

namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCurrencyById;

public class GetCurrencyByIdResponse
{
    [JsonProperty("value")]
    public CurrencyModel? Value { get; set; }

}
