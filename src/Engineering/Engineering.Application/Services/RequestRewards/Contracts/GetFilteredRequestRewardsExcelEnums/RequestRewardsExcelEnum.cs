using System.ComponentModel;

namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;

public enum RequestRewardsExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه درخواست")]
    Id = 1,

    [Description("وضعیت")]
    StatusDescription = 2,

    [Description("نوع")]
    TypeDescription = 3,

    [Description("طرف حساب ها")]
    ThirdParties = 4,

    [Description("شناسه مرکز هزینه")]
    CostCenterId = 5,

    [Description("مرکز هزینه")]
    CostCenterName = 6,

    [Description("شناسه پروژه")]
    ProjectId = 7,

    [Description("پروژه")]
    ProjectName = 8,

    [Description("شناسه شرح عملیات")]
    ProjectOperationId = 9,

    [Description("عنوان شرح عملیات")]
    OperationInfoName = 10,

    [Description("کد شرح عملیات")]
    OperationInfoCode = 11,

    [Description("شناسه ریزمتره")]
    ProjectOperationDetailId = 12,

    [Description("عنوان موقعیت جزئی")]
    PublicName = 13,

    [Description("کد موقعیت جزئی")]
    PublicCode = 14,

    [Description("شماره قرارداد")]
    ContractCode = 15,

    [Description("تاریخ")]
    RegistrationDateShamsi = 16,

    [Description("قیمت پیشنهادی")]
    OfferedPrice = 17,

    [Description("قیمت نهایی")]
    ConfirmedPrice = 18,

    [Description("توضیحات مدیر")]
    ManagerDescription = 19,

    [Description("شناسه ارز")]
    CurrencyId = 20,

    [Description("ارز")]
    Currency = 21,

    [Description("توضیحات")]
    Description = 22,

    [Description("شناسه موسسه")]
    CompanyId = 23,

    [Description("نام موسسه")]
    CompanyNameFa = 24
}