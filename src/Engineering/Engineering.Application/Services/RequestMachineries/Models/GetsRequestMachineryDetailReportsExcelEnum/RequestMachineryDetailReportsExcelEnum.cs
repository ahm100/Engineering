using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineries.Models.GetsRequestMachineryDetailReportsExcelEnum;

public enum RequestMachineryDetailReportsExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شماره درخواست ماشین آلات")]
    RequestMachineryId = 1,

    [Description("عنوان ماشین آلات")]
    MachineryName = 2,

    [Description("کد ماشین آلات")]
    MachineryCode = 3,

    [Description("مرکز هزینه")]
    CostCenterName = 4,

    [Description("پروژه")]
    ProjectName = 5,

    [Description("شرح عملیات ها")]
    ProjectOperations = 6,

    [Description("ریزمتره ها")]
    ProjectOperationDetails = 7,

    [Description("تاریخ ثبت")]
    Created = 8,

    [Description("گروه ماشین آلات")]
    MachineryGroupName = 9,

    [Description("زمان مورد نیاز")]
    TimeRequired = 10,

    [Description("واحد")]
    UnitDescription = 11,

    [Description("وضعیت")]
    StatusDescription = 12,

    [Description("تعداد درخواست")]
    RequestCount = 13,

    [Description("قیمت نهایی")]
    TotalPrice = 14,

    [Description("ارز")]
    Currency = 15,

    [Description("پیمانکار")]
    Contractor = 16,

    [Description("متصدی")]
    Operator = 17,

    [Description("از تاریخ")]
    FromDate = 18,

    [Description("از ساعت")]
    FromTime = 19,

    [Description("تا تاریخ")]
    ToDate = 20,

    [Description("تا ساعت")]
    ToTime = 21,

    [Description("تا تاریخ تایید")]
    ConfirmFromDate = 22,

    [Description("تا ساعت تایید")]
    ConfirmFromTime = 23,

    [Description("از تاریخ تایید")]
    ConfirmToDate = 24,

    [Description("از ساعت تایید")]
    ConfirmToTime = 25,

    [Description("تایید کننده")]
    ConfirmUser = 26,

    [Description("تاریخ تایید")]
    ConfirmDate = 27,

    [Description("ثبت کننده")]
    Creator = 28,

    [Description("زمان مورد تایید")]
    ConfirmedTimeRequired = 29,

    [Description("توضیحات تایید")]
    ConfirmedDescription = 30,

    [Description("شماره درخواست")]
    RequestNumber = 31,

    [Description("قیمت واحد")]
    UnitPrice = 32
}
