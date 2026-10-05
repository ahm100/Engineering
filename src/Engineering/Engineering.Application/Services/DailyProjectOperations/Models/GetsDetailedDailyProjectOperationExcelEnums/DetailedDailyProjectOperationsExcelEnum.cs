using System.ComponentModel;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDetailedDailyProjectOperationExcelEnums;

public enum DetailedDailyProjectOperationsExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("شناسه مرکز هزینه")]
    CostCenterId = 2,
    [Description("مرکز هزینه")]
    CostCenterName = 3,
    [Description("شناسه پروژه")]
    ProjectId = 4,
    [Description("نام پروژه")]
    ProjectName = 5,
    [Description("شناسه شرح عملیات")]
    ProjectOperationId = 6,
    [Description("شرح عملیات")]
    ProjectOperationName = 7,
    [Description("وضعیت شرح عملیات")]
    ProjectOperationStatusDescription = 8,
    [Description("شناسه واحد")]
    UnitOfMeasurementId = 9,
    [Description("واحد")]
    MeasurementName = 10,
    [Description("شناسه ریزمتره")]
    ProjectOperationDetailId = 11,
    [Description("کد ریزمتره")]
    ProjectOperationDetailCode = 12,
    [Description("طول ریزمتره")]
    ProjectOperationDetailLength = 13,
    [Description("عرض ریزمتره")]
    ProjectOperationDetailWidth = 14,
    [Description("ارتفاع ریزمتره")]
    ProjectOperationDetailHeight = 15,
    [Description("وزن ریزمتره")]
    ProjectOperationDetailWeight = 16,
    [Description("تعداد ریزمتره")]
    ProjectOperationDetailNumber = 17,
    [Description("حجم ریزمتره")]
    ProjectOperationDetailFinalAmount = 18,
    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 19,
    [Description("وضعیت ریزمتره")]
    ProjectOperationDetailStatusDescription = 20,
    [Description("تاریخ شروع")]
    ProjectOperationDetailStartDateShamsi = 21,
    [Description("تاریخ پایان")]
    ProjectOperationDetailEndDateShamsi = 22,
    [Description("تاریخ ساخت ریزمتره")]
    ProjectOperationDetailCreateDateShamsi = 23,
    [Description("شناسه آدرس")]
    OperationLocationId = 24,
    [Description("عنوان خصوصی")]
    PrivateName = 25,
    [Description("کد خصوصی")]
    PrivateCode = 26,
    [Description("عنوان عمومی")]
    PublicName = 27,
    [Description("کد عمومی")]
    PublicCode = 28,
    [Description("طول")]
    Length = 29,
    [Description("عرض")]
    Width = 30,
    [Description("ارتفاع")]
    Height = 31,
    [Description("وزن")]
    Weight = 32,
    [Description("تعداد")]
    Number = 33,
    [Description("حجم کارکرد")]
    DailyProjectOperationFinalAmount = 34,
    [Description("توضیحات")]
    Description = 35,
    [Description("وضعیت کارکرد")]
    StatusDescription = 36,
    [Description("تاریخ شروع کارکرد")]
    StartDateShamsi = 37,
    [Description("تاریخ پایان کارکرد")]
    EndDateShamsi = 38,
    [Description("پیمانکاران")]
    Contractors = 39,
    [Description("نام مستعار پیمانکاران")]
    ContractorsNickName = 40,
    [Description("شناسه ایجاد کننده")]
    CreatorId = 41,
    [Description("ایجاد کننده")]
    CreatorName = 42,
    [Description("نام مستعار ایجاد کننده")]
    CreatorNickname = 43,
    [Description("تاریخ ساخت کارکرد")]
    CreatedShamsi = 44,
    [Description("خدمات(واحد)")]
    ServiceInfos = 45,
}