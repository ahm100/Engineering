using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineryManagements.Models.RequestMachineryManagementExcelEnums;

public enum RequestMachineryManagementExcelEnum
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

    [Description("شناسه اپراتور")]
    AppointmentId = 20,

    [Description("اپراتور")]
    AppointmentFullName = 21,

    [Description("شناسه موسسه")]
    CompanyId = 22,

    [Description("نام موسسه")]
    CompanyNameFa = 23,

    [Description("ساعت شروع")]
    FromTime = 24,

    [Description("ساعت پایان")]
    ToTime = 25,

    [Description("تایید تاریخ شروع")]
    ConfirmFromDate = 26,

    [Description("تایید ساعت شروع")]
    ConfirmFromTime = 27,

    [Description("تایید تاریخ پایان")]
    ConfirmToDate = 28,

    [Description("تایید ساعت پایان")]
    ConfirmToTime = 29,

    [Description("توضیحات تغییر وضعیت")]
    ChangeStatusDescription = 30,

    [Description("زمان مورد نیاز تایید شده")]
    ConfirmedTimeRequired = 31,

    [Description("توضیحات تایید")]
    ConfirmedDescription = 32,

    [Description("پیمانکار")]
    Contractor = 33,

    [Description("شماره درخواست")]
    RequestNumber = 34,

    [Description("قیمت واحد")]
    UnitPrice = 35,

    [Description("قیمت کل")]
    TotalPrice = 36,

    [Description("نوع پرداخت")]
    PaymentType = 37
}