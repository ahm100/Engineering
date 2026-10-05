namespace Engineering.Application.WebServices.MetaDataServices.Currencies.Models.GetCitiesByCodes;

public record GetCitiesByCodesModel
{
    [JsonProperty("data")]
    public List<GetsCityByCodesModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}

public record GetsCityByCodesModel(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string Iso,
    long ProvinceId);