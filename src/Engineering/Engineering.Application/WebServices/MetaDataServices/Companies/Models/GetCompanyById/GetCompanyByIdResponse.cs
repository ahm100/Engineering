using CompanyModel = Engineering.Application.WebServices.MetaDataServices.Companies.Models.Company;

namespace Engineering.Application.WebServices.MetaDataServices.Companies.Models.GetCompanyById;

public record GetCompanyByIdResponse
{
    [JsonProperty("value")]
    public CompanyModel? Value { get; set; }
}