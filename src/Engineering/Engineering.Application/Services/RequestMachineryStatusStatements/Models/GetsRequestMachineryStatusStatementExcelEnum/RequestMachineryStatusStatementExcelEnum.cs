using System.ComponentModel;

namespace Engineering.Application.Services.RequestMachineryStatusStatements.Models.GetsRequestMachineryStatusStatementExcelEnum;

public enum RequestMachineryStatusStatementExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه صورت وضعیت")]
    Id = 1,

    [Description("مرکز هزینه")]
    CostCenter = 2,

    [Description("پروژه")]
    Project = 3,

    [Description("پیمانکار")]
    Contractor = 4,

    [Description("نام مستعار پیمانکار")]
    ContractorNickname = 5,

    [Description("شماره شبا پیمانکار")]
    ContractorIBAN = 6,

    [Description("از تاریخ")]
    FromDateShamsi = 7,

    [Description("تا تاریخ")]
    ToDateShamsi = 8,

    [Description("وضعیت")]
    StatusDescription = 9,

    [Description("کل تعداد درخواست")]
    TotalRequestedCount = 10,

    [Description("کل مبلغ محاسبه شده")]
    TotalFinalPrice = 11,

    [Description("مبلغ محاسبه شده پیمانکار")]
    ContractorFinalPrice = 12,

    [Description("توضیحات")]
    Description = 13,

    [Description("تاریخ ثبت")]
    Created = 14,

    [Description("تاریخ پرداخت")]
    PaymentDate = 15
}
