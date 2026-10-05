using System.ComponentModel;

namespace Engineering.Application.Services.Machineries.Models.GetsMachineryExcelEnum;

public enum MachineryExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام ماشین آلات")]
    MachineryName = 1,

    [Description("کد ماشین آلات")]
    MachineryCode = 2,

    [Description("نام گروه ماشین آلات")]
    GroupName = 3,

    [Description("کد گروه ماشین آلات")]
    GroupCode = 4,

    [Description("وضعیت")]
    IsActive = 5,

    [Description("نام موسسه")]
    CompanyNameFa = 6,
}