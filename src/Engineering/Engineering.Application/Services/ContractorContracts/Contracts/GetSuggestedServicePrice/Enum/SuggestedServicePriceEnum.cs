using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetSuggestedServicePrice.Enum;

public enum SuggestedServicePriceEnum
{
    [DefaultHeader]
    [Description("ردیف")]
    Id = 1,

    [DefaultHeader]
    [Description("خدمت")]
    Names = 2,

    [DefaultHeader]
    [Description("قیمت")]
    Price = 3,

    [DefaultHeader]
    [Description("ارز")]
    Currency = 4,

    [DefaultHeader]
    [Description("نام پیمانکار")]
    ContractorName = 5,

    [DefaultHeader]
    [Description("مرکز هزینه")]
    CostCenterName = 6,

    [DefaultHeader]
    [Description("پروژه")]
    ProjectName = 7,

    [DefaultHeader]
    [Description("نوع قرارداد پیمانکار")]
    ContractorContractType = 8,

    [DefaultHeader]
    [Description("تاریخ شروع")]
    StartDateShamsi = 9,

    [DefaultHeader]
    [Description("تاریخ پایان")]
    EndDateShamsi = 10,

    [DefaultHeader]
    [Description("ایجاد کننده درخواست")]
    Creator = 11,

    [DefaultHeader]
    [Description("تاریخ ایجاد")]
    CreatedShamsi = 12,

    [DefaultHeader]
    [Description("بروزرسانی کننده درخواست")]
    Updater = 13,

    [DefaultHeader]
    [Description("تاریخ بروزرسانی")]
    UpdatedShamsi = 14,
}