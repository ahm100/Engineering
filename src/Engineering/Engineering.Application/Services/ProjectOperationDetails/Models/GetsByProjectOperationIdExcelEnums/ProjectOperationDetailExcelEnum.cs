using System.ComponentModel;
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsByProjectOperationIdExcelEnums;

public enum ProjectOperationDetailExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه ریزمتره")]
    Id = 1,

    [Description("شناسه موقعیت")]
    OperationLocationId = 2,

    [Description("عنوان موقعیت جزئی")]
    PublicName = 3,

    [Description("کد موقعیت جزئی")]
    PublicCode = 4,

    [Description("شناسه شرح عملیات پروژه")]
    ProjectOperationId = 5,

    [Description("شناسه شرح عملیات")]
    OperationInfoId = 6,

    [Description("شرح عملیات")]
    OperationInfoName = 7,

    [Description("شناسه پروژه")]
    ProjectId = 8,

    [Description("پروژه")]
    ProjectName = 9,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 10,

    [Description("مرکز هزینه")]
    CostCenterName = 11,

    [Description("شناسه مدیر پروژه")]
    ProjectManagerId = 12,

    [Description("مدیر پروژه")]
    ProjectManagerName = 13,

    [Description("تاریخ شروع")]
    StartDate = 14,

    [Description("تاریخ پایان")]
    EndDate = 15,

    [Description("طول")]
    Length = 16,

    [Description("عرض")]
    Width = 17,

    [Description("ارتفاع")]
    Height = 18,

    [Description("وزن")]
    Weight = 19,

    [Description("تعداد")]
    Number = 20,

    [Description("مقدار نهایی")]
    FinalAmount = 21,

    [Description("وضعیت")]
    StatusDescription = 22,

    [Description("اولویت")]
    Priority = 23,

    [Description("روز")]
    Day = 24,

    [Description("زمان")]
    Hour = 25,

    [Description("توضیحات")]
    Description = 26,

    [Description("شناسه ثبت کننده")]
    CreatorId = 27,

    [Description("ثبت کننده")]
    Creator = 28,

    [Description("شناسه تغییر دهنده")]
    UpdatorId = 29,

    [Description("تغییر دهنده")]
    Updator = 30,

    [Description("تاریخ ایجاد")]
    CreateDate = 31,

    [Description("تاریخ اخرین تغییر")]
    UpdateDate = 32,

    [Description("شناسه موسسه")]
    CompanyId = 33,

    [Description("نام موسسه")]
    CompanyNameFa = 34,

    [Description("کد ریزمتره")]
    Code = 35,

    [Description("عنوان خصوصی")]
    PrivateName = 36,

    [Description("کد خصوصی")]
    PrivateCode = 37,

    [Description("حجم کار شده")]
    UsedFinalAmount = 38,

    [Description("پیمانکاران")]
    Contractors = 39,

    [Description("نام مستعار پیمانکاران")]
    ContractorNicknames = 40,

    [Description("حجم شرح عملیات")]
    WorkLoad = 41
}