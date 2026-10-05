using System.ComponentModel;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsContractorReportsExcelEnum;

public enum ContractorReportsExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه پیمانکار")]
    ContractorId = 1,

    [Description("پیمانکار")]
    Contractor = 5,

    [Description("شناسه")]
    Id = 10,

    [Description("شناسه نوع قرارداد پیمانکار")]
    ContractorContractTypeId = 15,

    [Description("نوع قرارداد پیمانکار")]
    ContractorContractTypeName = 20,

    [Description("کد نوع قرارداد پیمانکار")]
    ContractorContractTypeCode = 25,

    [Description("وضعیت")]
    StatusDescription = 30,

    [Description("شناسه ارز")]
    CurrencyId = 35,

    [Description("ارز")]
    Currency = 40,

    [Description("تاریخ شروع")]
    StartDate = 45,

    [Description("تاریخ پایان")]
    EndDate = 50,

    [Description("حجم کل")]
    TotalAmount = 55,

    [Description("درصد انجام کار")]
    PercentageDoingJobWell = 60,

    [Description("حجم کار انجام شده")]
    DoingJobWellAmount = 65,

    [Description("درصد پیش پرداخت")]
    PercentageAdvancePayment = 70,

    [Description("میزان پیش پرداخت")]
    AdvancePaymentAmount = 75,

    [Description("تاریخ دیرکرد برحسب روز")]
    DailyLatenessPenalty = 80,

    [Description("درصد کار تمام شده")]
    WorkDonePercent = 85,

    [Description("درصد کار تحویل شده")]
    WorkDeliveryPercent = 90,

    [Description("درصد کار پایان یافته")]
    WorkCompletionPercent = 95,

    [Description("توضیحات")]
    Description = 100,

    [Description("شناسه ایجاد کننده")]
    CreatorId = 105,

    [Description("ایجادکننده")]
    Creator = 110,

    [Description("تاریخ ایجاد")]
    Created = 115
}
