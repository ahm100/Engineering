using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs.Enum;

public enum GetDraftedFixCCsExcelEnum
{
    [Description("لیست قراردادهای پیمانکار")]
    Contracts = 1,

    [Description("لیست خدمات روزانه")]
    DailyServices = 2
}

public enum GetDraftedFixCCsEnum
{
    [DefaultHeader]
    [Description("شناسه")]
    Id = 1,

    [DefaultHeader]
    [Description("شناسه سربرگ قرارداد پیمانکار")]
    ContractorContractHeaderId = 2,

    [DefaultHeader]
    [Description("توضیحات سربرگ قرارداد پیمانکار")]
    ContractorContractHeaderDescription = 3,

    [DefaultHeader]
    [Description("نوع قرارداد پیمانکار")]
    ContractorContractType = 4,

    [DefaultHeader]
    [Description("کد نوع قرارداد پیمانکار")]
    ContractorContractTypeCode = 5,

    [DefaultHeader]
    [Description("شناسه پروژه")]
    ProjectId = 6,

    [DefaultHeader]
    [Description("شناسه پیمانکار")]
    ContractorId = 7,

    [DefaultHeader]
    [Description("تاریخ شروع")]
    StartDate = 8,

    [DefaultHeader]
    [Description("تاریخ پایان")]
    EndDate = 9,

    [DefaultHeader]
    [Description("مبلغ کل")]
    TotalAmount = 10,

    [DefaultHeader]
    [Description("درصد انجام کار خوب")]
    PercentageDoingJobWell = 11,

    [DefaultHeader]
    [Description("مبلغ انجام کار خوب")]
    DoingJobWellAmount = 12,

    [DefaultHeader]
    [Description("درصد پیش پرداخت")]
    PercentageAdvancePayment = 13,

    [DefaultHeader]
    [Description("مبلغ پیش پرداخت")]
    AdvancePaymentAmount = 14,

    [DefaultHeader]
    [Description("جریمه تاخیر روزانه")]
    DailyLatenessPenalty = 15,

    [DefaultHeader]
    [Description("توضیحات")]
    Description = 16,

    [DefaultHeader]
    [Description("شناسه‌های خدمات پیمانکار POD")]
    PODContractorServiceIds = 17
}

public enum GetDraftedFixDailiesEnum
{
    [DefaultHeader]
    [Description("شناسه")]
    Id = 1,

    [DefaultHeader]
    [Description("شناسه روزانه")]
    DailyId = 2,

    [DefaultHeader]
    [Description("شناسه عملیات پروژه")]
    ProjectOperationId = 3,

    [DefaultHeader]
    [Description("نام اطلاعات عملیات")]
    OperationInfoName = 5,

    [DefaultHeader]
    [Description("کد اطلاعات عملیات")]
    OperationInfoCode = 6,

    [DefaultHeader]
    [Description("شناسه واحد عملیات پروژه")]
    ProjectOperationMeasureId = 7,

    [DefaultHeader]
    [Description("مقدار عملیات پروژه")]
    ProjectOperationWorkload = 8,

    [DefaultHeader]
    [Description("واحد اندازه‌گیری عملیات پروژه")]
    ProjectOperationMeasurement = 9,

    [DefaultHeader]
    [Description("شناسه جزئیات خدمت پیمانکار در عملیات پروژه")]
    ProjectOperationDetailContractorServiceId = 10,

    [DefaultHeader]
    [Description("شناسه اطلاعات خدمت")]
    ServiceInfoId = 11,

    [DefaultHeader]
    [Description("نام خدمت")]
    ServiceInfoName = 12,

    [DefaultHeader]
    [Description("کد خدمت")]
    ServiceInfoCode = 13,

    [DefaultHeader]
    [Description("شناسه واحد خدمت")]
    ServiceInfoMeasureId = 14,

    [DefaultHeader]
    [Description("واحد اندازه‌گیری خدمت")]
    ServiceInfoMeasurement = 15,

    [DefaultHeader]
    [Description("طول")]
    Length = 16,

    [DefaultHeader]
    [Description("عرض")]
    Width = 17,

    [DefaultHeader]
    [Description("ارتفاع")]
    Height = 18,

    [DefaultHeader]
    [Description("وزن")]
    Weight = 19,

    [DefaultHeader]
    [Description("تعداد")]
    Number = 20,

    [DefaultHeader]
    [Description("مقدار نهایی")]
    FinalAmount = 21,

    [DefaultHeader]
    [Description("کد عمومی")]
    PublicCode = 22,

    [DefaultHeader]
    [Description("نام عمومی")]
    PublicName = 23,

    [DefaultHeader]
    [Description("توضیحات")]
    Description = 24,

    [DefaultHeader]
    [Description("حجم")]
    Volume = 25,

    [DefaultHeader]
    [Description("آدرس فایل‌ها")]
    Urls = 26,

    [DefaultHeader]
    [Description("دارای مستندات")]
    HaveDocuments = 27,

    [DefaultHeader]
    [Description("تاریخ ایجاد")]
    Created = 28,

    [DefaultHeader]
    [Description("شناسه ایجادکننده")]
    CreatorId = 29,

    [DefaultHeader]
    [Description("ایجادکننده")]
    Creator = 30
}
