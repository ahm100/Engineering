using System.ComponentModel;

namespace Engineering.Application.Services.MachineriesGroups.Models.GetsMachineriesGroupExcelEnum;

public enum MachineriesGroupExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام گروه ماشین'")]
    GroupName = 1,

    [Description("کد گروه ماشین")]
    GroupCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4,
}
