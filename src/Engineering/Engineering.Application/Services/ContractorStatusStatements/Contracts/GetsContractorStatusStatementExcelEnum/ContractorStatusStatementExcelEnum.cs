using System.ComponentModel;

namespace Engineering.Application.Services.ContractorStatusStatements.Models.GetsContractorStatusStatementExcelEnum;

public enum ContractorStatusStatementExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شماره صورت وضعیت")]
    Id = 1,

    [Description("پروژه")]
    Project = 2,

    [Description("پیمانکار")]
    Contractor = 3,

    [Description("نام مستعار")]
    Nickname = 4,

    [Description("ارز")]
    Currency = 5,

    [Description("کد")]
    Code = 6,

    [Description("تاریخ شروع")]
    StartDate = 7,

    [Description("تاریخ پایان")]
    EndDate = 8,

    [Description("وضعیت")]
    StatusDescription = 9,

    [Description("مقدار مبلغ نهایی قراردادها")]
    FinalTotalAmount = 10,

    [Description("مجموع درصد حسن انجام کار")]
    TotalPercentageDoingJobWell = 11,

    [Description("مجموع حسن انجام کار")]
    TotalDoingJobWellAmount = 12,

    [Description("کل مبلغ پیش پرداخت")]
    TotalAdvancePaymentAmount = 13,

    [Description("مجموع درصد پیش پرداخت")]
    TotalPercentageAdvancePayment = 14,

    [Description("مجموع جریمه دیرکرد روزانه")]
    TotalDailyLatenessPenalty = 15,

    [Description("مجموع درصد انجام کار")]
    TotalWorkDonePercent = 16,

    [Description("درصد کار باقی مانده")]
    TotalWorkDeliveryPercent = 17,

    [Description("درصد کار پایان یافته")]
    TotalWorkCompletionPercent = 18,

    [Description("مقدار کالاها")]
    ProductsAmount = 19,

    [Description("مبلغ کل جریمه ها")]
    FinesAmount = 20,

    [Description("مبلغ کل پاداش ها")]
    RewardsAmount = 21,

    [Description("مبلغ کل کارگران")]
    ThirdPartiesAmount = 22,

    [Description("مبلغ پرداختی")]
    PaymentedAmount = 23,

    [Description("توضیحات")]
    Description = 24,

    [Description("توضیحات کارشناس ارشد")]
    ManagmentDescription = 25
}
