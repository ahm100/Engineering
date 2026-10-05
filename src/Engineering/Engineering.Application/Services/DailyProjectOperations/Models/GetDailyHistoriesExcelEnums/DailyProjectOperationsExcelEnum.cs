using System.ComponentModel;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetDailyHistoriesExcelEnums;

public enum DailyProjectOperationsExcelEnum
{
    [Description("شناسه کارکرد روزانه")]
    DailyProjectOperationId = 1,
    [Description("شناسه شرح عملیات")]
    ProjectOperationId = 2,
    [Description("شرح عملیات")]
    OperationInfoName = 3,
    [Description("شناسه پروژه")]
    ProjectId = 4,
    [Description("نام پروژه")]
    ProjectName = 5,
    [Description("شناسه مرکزهزینه")]
    CostCenterId = 6,
    [Description("نام مرکزهزینه")]
    CostCenterName = 7,
    [Description("تاریخ کارکرد")]
    StartDate = 8,
    [Description("وضعیت")]
    StatusDescription = 9,
    [Description("حجم انجام شده")]
    FinalAmount = 10,
    [Description("شناسه واحد")]
    UnitOfMeasurementId = 11,
    [Description("واحد")]
    UnitOfMeasurement = 12,
    [Description("تاریخ ساخت")]
    Created = 13,
    [Description("شناسه موسسه")]
    CompanyId = 14,
    [Description("نام موسسه")]
    CompanyNameFa = 15,
    [Description("شناسه ریزمتره")]
    ProjectOperationDetailId = 16,
    [Description("موقعیت کلی")]
    PublicName = 17,
    [Description("کد موقعیت کلی")]
    PublicCode = 18,
    [Description("موقعیت جزیی")]
    PrivateName = 19,
    [Description("کد موقعیت جزیی")]
    PrivateCode = 20,
    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 21,
    [Description("توضیحات کارکرد روزانه")]
    Description = 24,
}