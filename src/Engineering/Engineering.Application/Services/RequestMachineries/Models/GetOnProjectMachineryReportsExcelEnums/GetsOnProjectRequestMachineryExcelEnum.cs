using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectMachineryReportsExcelEnums;

public enum GetsOnProjectRequestMachineryExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("مرکز هزینه")]
    CostCenterName = 1,

    [Description("پروژه")]
    ProjectName = 2,

    [Description("پیمانکار")]
    Contractor = 3,

    [Description("عنوان ماشین آلات")]
    MachineryName = 4,

    [Description("کد ماشین آلات")]
    Machinerycode = 5,

    [Description("واحد")]
    UnitDescription = 6,

    [Description("از تاریخ")]
    FromDateShamsi = 7,

    [Description("تا تاریخ")]
    ToDateShamsi = 8,

    [Description("مجموع  تعداد درخواستی")]
    TotalRequestedCount = 9,

    [Description("مجموع قیمت نهایی شده")]
    TotalFinalPrice = 10,

    [Description("مجموع زمان تایید شده")]
    TotalConfirmedTime = 11,

    [Description("تعداد روز تخصیص داده شده")]
    TotalDayWork = 12,

    [Description("مجموع زمان تخصیص داده شده")]
    TotalTimeWork = 13
}