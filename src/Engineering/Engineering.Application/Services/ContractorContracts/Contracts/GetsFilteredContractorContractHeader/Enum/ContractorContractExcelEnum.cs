using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader.Enum;

public enum ContractorContractExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("تاریخ شروع")]
    StartDate = 2,
    [Description("تاریخ پایان")]
    EndDate = 3,
    [Description("وضعیت قرارداد")]
    StatusDescription = 4,
    [Description("نوع قرارداد")]
    ContractorContractType = 5,
    [Description("شناسه پیمانکار")]
    ContractorId = 6,
    [Description("نام پیمانکار")]
    Contractor = 7,
    [Description("شناسه ایجادکننده")]
    CreatorId = 8,
    [Description("نام ایجادکننده")]
    Creator = 9,
    [Description("قیمت")]
    Price = 10,
    [Description("شناسه ارز")]
    CurrencyId = 11,
    [Description("نام ارز")]
    Currency = 12,
    [Description("کار انجام شده")]
    WorkDonePercent = 13,
    [Description("کاردرحال انجام")]
    WorkDeliveryPercent = 14,
    [Description("کار پایان یافته")]
    WorkCompletionPercent = 15,
    [Description("توضیحات")]
    Description = 16,
    [Description("شناسه موسسه")]
    CompanyId = 17,
    [Description("نام موسسه")]
    CompanyNameFa = 18
}
