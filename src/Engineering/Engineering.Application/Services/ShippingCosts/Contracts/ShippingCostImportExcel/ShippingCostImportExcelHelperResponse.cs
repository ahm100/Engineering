using System.ComponentModel;

namespace Engineering.Application.Services.ShippingCosts.Contracts.ShippingCostImportExcelHelper;

public record ShippingCostImportExcelHelperResponse(
    FileContentResult File
    );

public enum ShippingCostImportExcelHelper
{
    [Description("MachineTypeCode - کد ماشین")] MachineTypeCode = 1,
    [Description("SourceCityCode - کد شهر مبدا")] SourceCityCode = 2,
    [Description("DestinationCityCode - کد شهر مقصد")] DestinationCityCode = 3,
    [Description("Price - مبلغ")] Price = 4,
    [Description("FromDate - از تاریخ")] FromDate = 5,
    [Description("ToDate - تا تاریخ")] ToDate = 6,
    [Description("ThirdPartyCode - کد سازمانی طرف حساب")] ThirdPartyCode = 7,
    [Description("Count - تعداد")] Count = 8,
    [Description("LoadWeight - وزن")] LoadWeight = 9,
    [Description("RegionCode - کد ناحیه")] RegionCode = 10,
    [Description("Tax - مالیات")] Tax = 11,
    [Description("Description -  توضیحات")] Description = 12,
}
public enum CityImportExcelModel
{
    [Description("نام شهر")] Name = 1,
    [Description("کد شهر")] Code = 2
}

public enum RegionsImportExcelModel
{
    [Description("نام ناحیه")] Name = 1,
    [Description("کد ناحیه")] Code = 2,
    [Description("شهر")] CityName = 3,
    [Description("کد شهر")] CityCode = 4
}

public enum MachineTypeImportExcelModel
{
    [Description("نام ماشین")] Name = 1,
    [Description("کد ماشین")] Code = 2,
    [Description("نوع کابین")] CabinTypeName = 3,
    [Description("کد کابین")] CabinTypeCode = 4,
}

public class ViewCityDataModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
}

public class ViewRegionDataModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? CityName { get; set; }
    public string? CityCode { get; set; }
}

public class MachineTypeDataModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? CabinTypeName { get; set; }
    public string? CabinTypeCode { get; set; }
}