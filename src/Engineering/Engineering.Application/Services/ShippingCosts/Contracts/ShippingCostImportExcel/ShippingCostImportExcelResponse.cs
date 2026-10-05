using System.ComponentModel;

namespace Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcel;

public record ShippingCostImportExcelResponse(
    FileContentResult File
    );

public enum ShippingCostImportExcel
{
    [Description("MachineTypeCode")] MachineTypeCode = 1,
    [Description("SourceCityCode")] SourceCityCode = 2,
    [Description("DestinationCityCode")] DestinationCityCode = 3,
    [Description("Price")] Price = 4,
    [Description("FromDate")] FromDate = 5,
    [Description("ToDate")] ToDate = 6,
    [Description("ThirdPartyCode")] ThirdPartyCode = 7,
    [Description("ThirdPartyCompanyCode")] ThirdPartyCompanyCode = 8,
    [Description("Count")] Count = 9,
    [Description("LoadWeight")] LoadWeight = 10,
    [Description("RegionCode")] RegionCode = 11,
    [Description("Tax")] Tax = 12,
    [Description("Description")] Description = 13,
}