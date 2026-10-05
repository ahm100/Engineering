using Engineering.Domain.Entities.MachineTypes;
using Engineering.Domain.Entities.Synonyms.MetaData.Cities;
using Engineering.Domain.Entities.Synonyms.MetaData.Regions;
using Engineering.Domain.Entities.Synonyms.MetaData.ThirdParties;
using IdentityServer.ClientSdk.Models.ThirdParty;

namespace Engineering.Application.Services.ShippingCosts.Models.ShippingCostExcelImports;

public record ShippingCostExcelImportsModel
{
    public string MachineTypeCode { get; set; } = string.Empty;
    public string? SourceCityCode { get; set; } = string.Empty;
    public string? DestinationCityCode { get; set; } = string.Empty;
    public string Price { get; set; } = string.Empty;
    public string? FromDate { get; set; }
    public string? ToDate { get; set; }
    public string? ThirdPartyCode { get; set; }
    public string? ThirdPartyCompanyCode { get; set; }
    public string? Count { get; set; }
    public string? LoadWeight { get; set; }
    public string? RegionCode { get; set; }
    public string? Tax { get; set; }
    public string? Description { get; set; }
}

public record GetImportsExcelDataModel
{
    public List<MachineType>? MachineTypes { get; set; }
    public List<ViewCity>? SourceCities { get; set; }
    public List<ViewCity>? DestinationCities { get; set; }
    public List<ViewRegion>? Regions { get; set; }
    public List<ViewThirdParty>? ThirdParties { get; set; }
    public List<GetCompaniesByCodesResponseModel>? ThirdPartiesCompany { get; set; }
}