using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetFilteredCompaniesByIds;

public record GetFilteredCompaniesByIdsModel
{
    [JsonProperty("data")]
    public List<CompanyModel>? Data { get; set; }

    [JsonProperty("rowCount")]
    public int RowCount { get; set; }
}