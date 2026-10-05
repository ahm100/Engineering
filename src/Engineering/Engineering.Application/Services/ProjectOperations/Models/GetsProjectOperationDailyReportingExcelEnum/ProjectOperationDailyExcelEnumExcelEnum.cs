using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationDailyReportingExcelEnum;

public enum ProjectOperationDailyExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 2,

    [Description("مرکزهزینه")]
    CostCenterName = 3,

    [Description("کد مرکزهزینه")]
    CostCenterCode = 4,

    [Description("شناسه پروژه")]
    ProjectId = 5,

    [Description("پروژه")]
    ProjectName = 6,

    [Description("کد پروژه")]
    ProjectCode = 7,

    [Description("شناسه شرح عملیات")]
    OperationInfoId = 8,

    [Description("شرح عملیات")]
    OperationInfoName = 9,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 10,

    [Description("شناسه واحد سنجش شرح عملیات")]
    OperationInfoMeasurementId = 11,

    [Description("واحد سنجش شرح عملیات")]
    OperationInfoMeasurementName = 12,

    [Description("قیمت ریالی")]
    RialPrice = 13,

    [Description("قیمت دلاری")]
    DollarPrice = 14,

    [Description("قیمت کل ریالی")]
    TotalRialPrice = 15,

    [Description("قیمت کل دلاری")]
    TotalDollarPrice = 16,

    [Description("حجم")]
    Workload = 17,

    [Description("حجم تمام شده")]
    DoneWorkload = 18,

    [Description("حجم باقی مانده")]
    RemaindedWorkload = 19,

    [Description("توضیحات")]
    Description = 20
}

public enum ProjectOperationDailyDetailExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه کارکرد")]
    ProjectOperationId = 2,

    [Description("شرح عملیات")]
    OperationInfoName = 3,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 4,

    [Description("واحد سنجش شرح عملیات")]
    OperationInfoMeasurementName = 5,

    [Description("پروژه")]
    ProjectName = 6,

    [Description("کد پروژه")]
    ProjectCode = 7,

    [Description("مکان")]
    Location = 8,

    [Description("طول")]
    Length = 9,

    [Description("ارتفاع")]
    Height = 10,

    [Description("عرض")]
    Width = 11,

    [Description("وزن")]
    Weight = 12,

    [Description("تعداد")]
    Number = 13,

    [Description("توضیحات")]
    Description = 14
}