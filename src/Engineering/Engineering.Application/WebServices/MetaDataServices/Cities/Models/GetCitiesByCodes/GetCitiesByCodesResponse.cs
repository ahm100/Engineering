namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;

public record GetCitiesByCodesResponse
{
    [JsonProperty("value")]
    public GetCitiesByCodesModel? Value { get; set; }
}