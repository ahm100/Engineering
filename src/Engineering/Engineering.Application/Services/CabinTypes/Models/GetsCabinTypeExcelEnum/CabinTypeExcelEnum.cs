using System.ComponentModel;

namespace Engineering.Application.Services.CabinTypes.Models.GetsCabinTypeExcelEnum;

public enum CabinTypeExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام نوع اتاق")]
    CabinTypeName = 1,

    [Description("کد نوع اتاق")]
    CabinTypeCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4
}