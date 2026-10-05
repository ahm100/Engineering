using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetConfirmedCCDailyServices.Enum;

public enum GetConfirmedCCDailyServicesEnum
{
    [Description("شناسه")]
    Id = 1,

    [Description("شماره قرارداد")]
    ContractorContractCode = 2,

    [Description("حجم انجام شده خدمت در این کارکرد")]
    Volume = 3,

    [Description("حجم قرارداد خدمت")]
    TotalServiceVolume = 4,

    [Description("قیمت واحد")]
    UnitPrice = 5,

    [Description("قیمت مجموع")]
    TotalPrice = 6,

    [Description("نام خدمات")]
    ServiceInfoName = 7,

    [Description("کد خدمات")]
    ServiceInfoCode = 8,

    [Description("واحد انداز‌گیری خدمات")]
    ServiceInfoMeasure = 9,

    [Description("پیمانکار")]
    Contractor = 10,

    [Description("نام مستعار پیمانکار")]
    ContractorNickName = 11,

    [Description("نام مرکز هزینه")]
    CostCenterName = 12,

    [Description("نام پروژه")]
    ProjectName = 13,

    [Description("نام شرح عملیات")]
    ProjectOperationName = 14,

    [Description("حجم شرح عملیات")]
    Workload = 15,

    [Description("نام واحدشرح عملیات")]
    ProjectOperationMeasure = 16,

    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 17,

    [Description("وضعیت ریزمتره")]
    ProjectOperationDetailStatusDescription = 18,

    [Description("نام ریزمتره")]
    PrivateName = 19,

    [Description("کد ریزمتره")]
    PrivateCode = 20,

    [Description("نام عمومی ریزمتره")]
    PublicName = 21,

    [Description("کد عمومی ریزمتره")]
    PublicCode = 22,

    [Description("حجم ریزمتره")]
    FinalAmount = 22,

    [Description("تاریخ شروع قرارداد ")]
    StartDateShamsi = 23,

    [Description("تاریخ پایان قرارداد ")]
    EndDateShamsi = 24,

    [Description("نام ایجادکننده")]
    CreatorName = 25,

    [Description("تاریخ ایجاد")]
    CreatedShamsi = 26,
}

public enum GetConfirmedCCDailyServicesOtherEnum
{
    [Description("حجم کل قرارداد")]
    TotalVolume = 1,
    [Description("حجم انجام شده")]
    DoneVolume = 2,
    [Description("حجم باقی مانده")]
    RemainVolume = 3,
    [Description("مجموع قیمت انجام شده")]
    DonePriced = 4,
    [Description("مجموع قیمت قرارداد")]
    ContractPrice = 5,
}