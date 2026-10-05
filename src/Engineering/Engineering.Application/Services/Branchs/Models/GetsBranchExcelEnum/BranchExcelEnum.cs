using System.ComponentModel;

namespace Engineering.Application.Services.Branchs.Models.GetsBranchExcelEnum;

public enum BranchExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام رشته")]
    BranchName = 1,

    [Description("کد رشته")]
    BranchCode = 2,

    [Description("نام رسته")]
    CategoryName = 3,

    [Description("کد رسته")]
    CategoryCode = 4,

    [Description("وضعیت")]
    IsActive = 5,

    [Description("نام موسسه")]
    CompanyNameFa = 6
}