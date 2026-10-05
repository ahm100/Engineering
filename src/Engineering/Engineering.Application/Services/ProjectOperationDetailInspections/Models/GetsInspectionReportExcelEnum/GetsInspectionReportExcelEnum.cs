using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReportExcelEnum;

public enum InspectionReportExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("نام مرکز هزینه")]
    CostCenterName = 2,
    [Description("کد مرکز هزینه")]
    CostCenterCode = 3,
    [Description("نام پروژه")]
    ProjectName = 4,
    [Description("کد پروژه")]
    ProjectCode = 5,
    [Description("نام شرح عملیات")]
    OperationInfoName = 6,
    [Description("کد شرح عملیات")]
    OperationInfoCode = 7,
    [Description("حجم کار شرح عملیات")]
    Workload = 8,
    [Description("کد ریزمتره")]
    ProjectOperationDetailCode = 9,
    [Description("نام عمومی")]
    PublicName = 10,
    [Description("کد عمومی")]
    PublicCode = 11,
    [Description("نام خصوصی")]
    PrivateName = 12,
    [Description("کد خصوصی")]
    PrivateCode = 13,
    [Description("طول ریزمتره")]
    DetailLength = 14,
    [Description("عرض ریزمتره")]
    DetailWidth = 15,
    [Description("ارتفاع ریزمتره")]
    DetailHeight = 16,
    [Description("وزن ریزمتره")]
    DetailWeight = 17,
    [Description("تعداد ریزمتره")]
    DetailNumber = 18,
    [Description("مقدار نهایی ریزمتره")]
    DetailFinalAmount = 19,
    [Description("طول")]
    Length = 20,
    [Description("عرض")]
    Width = 21,
    [Description("ارتفاع")]
    Height = 22,
    [Description("وزن")]
    Weight = 23,
    [Description("تعداد")]
    Number = 24,
    [Description("مقدار نهایی")]
    FinalAmount = 25,
    [Description("تاریخ بازرسی")]
    InspectionDateShamsi = 26,
    [Description("توضیحات ریزمتره")]
    DetailDescription = 27,
    [Description("توضیحات")]
    Description = 28,
    [Description("ایجادکننده")]
    Creator = 29,
    [Description("تاریخ ایجاد")]
    CreateDateShamsi = 30,
}