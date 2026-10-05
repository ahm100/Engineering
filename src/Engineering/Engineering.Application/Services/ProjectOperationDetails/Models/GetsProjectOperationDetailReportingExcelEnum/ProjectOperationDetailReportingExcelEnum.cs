using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailReportingExcelEnum;

public enum ProjectOperationDetailReportingExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه ریزمتره")]
    Id = 1,

    [Description("کد")]
    Code = 2,

    [Description("شناسه شرح عملیات پروژه")]
    ProjectOperationId = 3,

    [Description("شناسه شرح عملیات")]
    OperationInfoId = 4,

    [Description("شرح عملیات")]
    OperationInfoName = 5,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 6,

    [Description("شناسه واحد سنجش")]
    MeasurementId = 7,

    [Description("واحد سنجش")]
    MeasurementName = 8,

    [Description("شناسه پروژه")]
    ProjectId = 9,

    [Description("پروژه")]
    ProjectName = 10,

    [Description("کد پروژه")]
    ProjectCode = 11,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 12,

    [Description("مرکزهزینه")]
    CostCenterName = 13,

    [Description("کد مرکزهزینه")]
    CostCenterCode = 14,

    [Description("شناسه آدرس دهی")]
    OperationLocationId = 15,

    [Description("عنوان خصوصی")]
    PrivateName = 16,

    [Description("کد خصوصی")]
    PrivateCode = 17,

    [Description("عنوان عمومی")]
    PublicName = 18,

    [Description("کد عمومی")]
    PublicCode = 19,

    [Description("پیمانکاران")]
    Contractors = 20,

    [Description("نام مستعار پیمانکاران")]
    ContractorsNickName = 21,

    [Description("طول")]
    Length = 22,

    [Description("عرض")]
    Width = 23,

    [Description("ارتفاع")]
    Height = 24,

    [Description("وزن")]
    Weight = 25,

    [Description("تعداد")]
    Number = 26,

    [Description("حجم")]
    FinalAmount = 27,

    [Description("حجم تمام شده")]
    DoneFinalAmount = 28,

    [Description("حجم باقی مانده")]
    RemaindedFinalAmount = 29,

    [Description("وضعیت")]
    StatusDescription = 30,

    [Description("تاریخ شروع")]
    StartDate = 31,

    [Description("تاریخ پایان")]
    EndDate = 32,

    [Description("روز")]
    Day = 33,

    [Description("ساعت")]
    Hour = 34,

    [Description("الویت")]
    Priority = 35,

    [Description("شناسه ایجادکننده")]
    CreatorId = 36,

    [Description("ایجادکننده")]
    CreatorName = 37,

    [Description("نام مستعار ایجادکننده")]
    CreatorNickname = 38,

    [Description("تاریخ ایجاد")]
    Created = 39,

    [Description("شناسه ویرایش کننده")]
    UpdaterId = 40,

    [Description("ویرایش کننده")]
    UpdaterName = 41,

    [Description("نام مستعار ویرایش کننده")]
    UpdaterNickname = 42,

    [Description("تاریخ ویرایش")]
    Updated = 43,

    [Description("توضیحات")]
    Description = 44
}