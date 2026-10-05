using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperations.Models.GetsProjectOperationReportingExcelEnum;

public enum ProjectOperationExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه شرح عملیات")]
    OperationInfoId = 2,

    [Description("شرح عملیات")]
    OperationInfoName = 3,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 4,

    [Description("شناسه واحد سنجش شرح عملیات")]
    OperationInfoMeasurementId = 5,

    [Description("واحد سنجش شرح عملیات")]
    OperationInfoMeasurementName = 6,

    [Description("شناسه پروژه")]
    ProjectId = 7,

    [Description("پروژه")]
    ProjectName = 8,

    [Description("کد پروژه")]
    ProjectCode = 9,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 10,

    [Description("مرکزهزینه")]
    CostCenterName = 11,

    [Description("کد مرکزهزینه")]
    CostCenterCode = 12,

    [Description("رسته")]
    CategoryName = 13,

    [Description("رشته")]
    BranchName = 14,

    [Description("فصل")]
    SeasonName = 15,

    [Description("پیمانکاران")]
    Contractors = 16,

    [Description("نام مستعار پیمانکاران")]
    ContractorsNickName = 17,

    [Description("دستیاران پیاده سازی")]
    ImplementationAssistants = 18,

    [Description("نام مستعار دستیاران پیاده سازی")]
    ImplementationAssistantsNickName = 19,

    [Description("دستیاران فنی")]
    TechnicalAssistants = 20,

    [Description("نام مستعار دستیاران فنی")]
    TechnicalAssistantsNickName = 21,

    [Description("ایجاد کنندگان کارکرد روزانه")]
    DailyProjectOperationCreators = 22,

    [Description("تاریخ شروع")]
    StartDate = 23,

    [Description("تاریخ پایان")]
    EndDate = 24,

    [Description("شناسه واحد سنجش")]
    MeasurementId = 25,

    [Description("واحد سنجش")]
    MeasurementName = 26,

    [Description("درصد تلرانس")]
    TolerancePercentage = 27,

    [Description("قیمت")]
    Price = 28,

    [Description("الویت")]
    Priority = 29,

    [Description("وضعیت")]
    StatusDescription = 30,

    [Description("حجم")]
    Workload = 31,

    [Description("حجم تمام شده")]
    DoneWorkload = 32,

    [Description("حجم باقی مانده")]
    RemaindedWorkload = 33,

    [Description("شناسه ایجاد کننده")]
    CreatorId = 34,

    [Description("ایجاد کننده")]
    CreatorName = 35,

    [Description("تاریخ ایجاد")]
    Created = 36,

    [Description("توضیحات")]
    Description = 37
}