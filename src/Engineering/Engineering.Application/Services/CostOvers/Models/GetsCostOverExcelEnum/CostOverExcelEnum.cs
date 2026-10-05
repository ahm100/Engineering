using System.ComponentModel;

namespace Engineering.Application.Services.CostOvers.Models.GetsCostOverExcelEnum;

public enum CostOverExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام هزینه سربار")]
    CostOverName = 1,

    [Description("کد هزینه سربار")]
    CostOverCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4
}