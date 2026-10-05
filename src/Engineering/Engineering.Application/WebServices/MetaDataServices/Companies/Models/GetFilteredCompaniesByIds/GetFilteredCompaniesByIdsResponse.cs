namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetFilteredCompaniesByIds;

public record GetFilteredCompaniesByIdsResponse
{
    [JsonProperty("value")]
    public GetFilteredCompaniesByIdsModel? Value { get; set; }
}