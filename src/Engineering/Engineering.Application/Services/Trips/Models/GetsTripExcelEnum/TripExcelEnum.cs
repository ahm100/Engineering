using System.ComponentModel;

namespace Engineering.Application.Services.Trips.Models.GetsTripExcelEnum;

public enum TripExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام سفر")]
    TripName = 1,

    [Description("کد سفر")]
    TripCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4,
}
