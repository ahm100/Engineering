using System.ComponentModel;

namespace Engineering.Application.Services.ProjectTypes.Models.GetsProjectTypeExcelEnum;

public enum ProjectTypeExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("نام نوع پروژه")]
    ProjectTypeName = 1,

    [Description("کد نوع پروژه")]
    ProjectTypeCode = 2,

    [Description("وضعیت")]
    IsActive = 3,

    [Description("نام موسسه")]
    CompanyNameFa = 4
}
