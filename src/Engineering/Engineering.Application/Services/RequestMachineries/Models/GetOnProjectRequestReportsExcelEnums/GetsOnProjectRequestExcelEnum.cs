using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineries.Models.GetOnProjectRequestReportsExcelEnums;

public enum GetsOnProjectRequestExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه درخواست")]
    RequestMachineryId = 1,

    [Description("شماره درخواست")]
    RequestNumber = 2,

    [Description("گروه ماشین آلات")]
    MachineryGroupName = 3,

    [Description("ماشین آلات")]
    MachineryName = 4,

    [Description("کد ماشین آلات")]
    MachineryCode = 5,

    [Description("مرکز هزینه")]
    CostCenterName = 6,

    [Description("پروژه")]
    ProjectName = 7,

    [Description("شرح عملیات ها")]
    ProjectOperations = 8,

    [Description("ریزمتره ها")]
    ProjectOperationDetails = 9,

    [Description("تاریخ ایجاد درخواست")]
    Created = 10,

    [Description("زمان یا مقدار مورد نیاز")]
    TimeRequired = 11,

    [Description("واحد")]
    UnitDescription = 12,

    [Description("وضعیت")]
    StatusDescription = 13,

    [Description("تعداد")]
    RequestCount = 14,

    [Description("مبلغ")]
    TotalPrice = 15,

    [Description("ارز")]
    Currency = 16,

    [Description("پیمانکار")]
    Contractor = 17,

    [Description("متصدی")]
    Operator = 18,

    [Description("تاریخ شروع درخواست")]
    FromDate = 19,

    [Description("زمان شروع درخواست")]
    FromTime = 20,

    [Description("تاریخ پایان درخواست")]
    ToDate = 21,

    [Description("زمان پایان درخواست")]
    ToTime = 22,

    [Description("تاریخ شروع تایید شده")]
    ConfirmFromDate = 23,

    [Description("زمان شروع تایید شده")]
    ConfirmFromTime = 24,

    [Description("تاریخ پایان تایید شده")]
    ConfirmToDate = 25,

    [Description("زمان پایان تایید شده")]
    ConfirmToTime = 26,

    [Description("تایید کننده")]
    ConfirmUser = 27,

    [Description("تاریخ تایید")]
    ConfirmDate = 28,

    [Description("درخواست دهنده")]
    Creator = 29,

    [Description("زمان یا مقدار مورد نیاز تایید شده")]
    ConfirmedTimeRequired = 30,

    [Description("توضیخات تایید")]
    ConfirmedDescription = 31,

    [Description("قیمت واحد")]
    UnitPrice = 32,

    [Description("پرداخت")]
    PaymentType = 33
}