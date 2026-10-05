namespace Engineering.Application.WebServices.MetaDataServices.Cities.Models.GetProvinceById;

public class GetProvinceByIdResponse
{
    [JsonProperty("value")]
    public GetProvinceByIdResponseModel? Value { get; set; }
}

public record GetProvinceByIdResponseModel(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string Iso,
    GetProvinceByIdCountryModel Country,
    long? LegacyId);

public record GetProvinceByIdCountryModel(
    long Id,
    string Name,
    string Code,
    bool IsActive,
    string EnglishName,
    string Iso,
    string FlagUrl,
    long? LegacyId);