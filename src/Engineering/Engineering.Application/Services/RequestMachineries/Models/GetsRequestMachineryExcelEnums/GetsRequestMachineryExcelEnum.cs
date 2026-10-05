using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineries.Models.GetsEmployerStatusStatementExcelEnums;

public enum GetsRequestMachineryExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("تاریخ ایجاد")]
    Created = 2,

    [Description("گروه ماشین")]
    MachineryGroupName = 3,

    [Description("نام ماشین آلات")]
    MachineryName = 4,

    [Description("زمان مورد نیاز")]
    TimeRequired = 5,

    [Description("واحد")]
    UnitDescription = 6,

    [Description("وضعیت")]
    StatusDescription = 7,

    [Description("تعداد")]
    RequestCount = 8,

    [Description("تاریخ از")]
    FromDate = 9,

    [Description("تاریخ تا")]
    ToDate = 10,

    [Description("شناسه درخواست دهنده")]
    CreatorId = 11,

    [Description("درخواست دهنده")]
    Creator = 12,

    [Description("مرکزهزینه")]
    CostCenterName = 13,

    [Description("پروژه")]
    ProjectName = 14,

    [Description("شرح عملیات")]
    ProjectOperations = 15,

    [Description("ریزمتره")]
    ProjectOperationDetails = 16,

    [Description("تاریخ تایید")]
    ConfirmDate = 17,

    [Description("شناسه تایید کننده")]
    ConfirmUserId = 18,

    [Description("تایید کننده")]
    ConfirmUser = 19,

    [Description("شناسه موسسه")]
    CompanyId = 20,

    [Description("نام موسسه")]
    CompanyNameFa = 21,

    [Description("ساعت شروع")]
    FromTime = 22,

    [Description("ساعت پایان")]
    ToTime = 23,

    [Description("تایید تاریخ شروع")]
    ConfirmFromDate = 24,

    [Description("تایید ساعت شروع")]
    ConfirmFromTime = 25,

    [Description("تایید تاریخ پایان")]
    ConfirmToDate = 26,

    [Description("تایید ساعت پایان")]
    ConfirmToTime = 27,

    [Description("زمان مورد نیاز تایید شده")]
    ConfirmedTimeRequired = 28,

    [Description("توضیحات تایید")]
    ConfirmedDescription = 29,

    [Description("پیمانکار")]
    Contractor = 30,

    [Description("شماره درخواست")]
    RequestNumber = 31,

    [Description("مبلغ واحد")]
    UnitPrice = 32,

    [Description("مبلغ نهایی")]
    TotalPrice = 33,
}