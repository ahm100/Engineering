using System.ComponentModel;

namespace Engineering.Application.Services.CostCenters.Models.GetsCostCenterExcelEnum;

public enum CostCenterExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نوع مرکزهزینه")]
    CostCenterTypeTitle = 1,

    [Description("نام مرکزهزینه")]
    CostCenterName = 2,

    [Description("کد مرکزهزینه")]
    CostCenterCode = 3,

    [Description("روزهای بدون فعالیت")]
    NoOperationDays = 4,

    [Description("نام انبار")]
    WarehouseName = 5,

    [Description("مدیر انبار")]
    WarehouseManagement = 6,

    [Description("نام شهر")]
    CityName = 7,

    [Description("آدرس")]
    Address = 8,

    [Description("کد پستی")]
    PostalCode = 9,

    [Description("توضیحات")]
    Description = 10,

    [Description("ثبت وضعیت آب و هوا")]
    WeatherState = 11,

    [Description("وضعیت")]
    IsActive = 12,

    [Description("نام موسسه")]
    CompanyNameFa = 13
}