using System.ComponentModel;

namespace Engineering.Application.Services.Projects.Models.GetsProjectExcelEnum;

public enum ProjectExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه نوع پروژه")]
    ProjectTypeId = 2,

    [Description("نوع پروژه")]
    ProjectTypeTitle = 3,

    [Description("نام پروژه")]
    ProjectName = 4,

    [Description("کد پروژه")]
    ProjectCode = 5,

    [Description("شناسه کارفرما")]
    EmployerId = 6,

    [Description("کارفرما")]
    EmployerName = 7,

    [Description("شناسه رسته")]
    CategoryId = 8,

    [Description("رسته")]
    CategoryName = 9,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 10,

    [Description("مرکزهزینه")]
    CostCenterName = 11,

    [Description("شناسه ناظر")]
    SupervisorEngineer = 12,

    [Description("ناظر")]
    SupervisorEngineerName = 13,

    [Description("شناسه مشاور")]
    AdvisorId = 14,

    [Description("مشاور")]
    AdvisorName = 15,

    [Description("شناسه مدیرپروژه")]
    ProjectManagerId = 16,

    [Description("مدیرپروژه")]
    ProjectManagerName = 17,

    [Description("شناسه مسئول برنامه ریزی")]
    PlanningAssistantId = 18,

    [Description("مسئول برنامه ریزی")]
    PlanningAssistantName = 19,

    [Description("وضعیت پروژه")]
    StatusTitle = 20,

    [Description("وضعیت")]
    IsActive = 21,

    [Description("شناسه موسسه")]
    CompanyId = 22,

    [Description("نام موسسه")]
    CompanyNameFa = 23,

    [Description("قرارداد پذیری")]
    Contractual = 24,

    [Description("شهر")]
    City = 25,

    [Description("توضیحات پروژه")]
    Description = 26
}