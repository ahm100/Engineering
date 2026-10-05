using System.ComponentModel;

namespace Engineering.Application.Services.OperationInfoGroups.Models.GetsOperationInfoGroupExcelEnum;

public enum OperationInfoGroupExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام گروه شرح عملیات")]
    OperationInfoGroupName = 1,

    [Description("کد گروه شرح عملیات")]
    OperationInfoGroupCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4
}
