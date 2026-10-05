using System.ComponentModel;

namespace Engineering.Application.Services.Categories.Models.GetsCategoryExcelEnum;

public enum CategoryExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام رسته")]
    CategoryName = 1,

    [Description("کد رسته")]
    CategoryCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4
}