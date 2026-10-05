using System.ComponentModel;

namespace Engineering.Application.Services.CostCenterTypes.Models.GetsCostCenterTypeExcelEnum;

public enum CostCenterTypeExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام نوع مرکزهزینه")]
    CostCenterTypeTitle = 2,

    [Description("کد نوع مرکزهزینه")]
    CostCenterTypeCode = 3,

    [Description("وضعیت")]
    IsActive = 4,

    [Description("نام موسسه")]
    CompanyNameFa = 6
}