using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs.Enum
{
    public enum GetDraftedServiceCCsExcelEnum
    {
        [Description("لیست قراردادهای خدمات پیمانکار")]
        ContractorServiceContracts = 1,

        [Description("لیست خدمات روزانه")]
        DailyServices = 2
    }

    public enum GetDraftedServiceCCsEnum
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
        [Description("مبلغ کل کارکرد روزانه‌ها")]
        TotalWorkedAmount = 11,

        [DefaultHeader]
        [Description("درصد انجام کار خوب")]
        PercentageDoingJobWell = 12,

        [DefaultHeader]
        [Description("مبلغ انجام کار خوب")]
        DoingJobWellAmount = 13,

        [DefaultHeader]
        [Description("درصد پیش پرداخت")]
        PercentageAdvancePayment = 14,

        [DefaultHeader]
        [Description("مبلغ پیش پرداخت")]
        AdvancePaymentAmount = 15,

        [DefaultHeader]
        [Description("جریمه تاخیر روزانه")]
        DailyLatenessPenalty = 16,

        [DefaultHeader]
        [Description("توضیحات")]
        Description = 17,

        [DefaultHeader]
        [Description("شناسه‌های خدمات پیمانکار POD")]
        PODContractorServiceIds = 18
    }

    public enum GetDraftedServiceDailiesEnum
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
        [Description("قیمت واحد")]
        UnitPrice = 26,

        [DefaultHeader]
        [Description("مبلغ کل")]
        TotalAmount = 27,

        [DefaultHeader]
        [Description("آدرس فایل‌ها")]
        Urls = 28,

        [DefaultHeader]
        [Description("دارای مستندات")]
        HaveDocuments = 29,

        [DefaultHeader]
        [Description("تاریخ ایجاد")]
        Created = 30,

        [DefaultHeader]
        [Description("شناسه ایجادکننده")]
        CreatorId = 31,

        [DefaultHeader]
        [Description("ایجادکننده")]
        Creator = 32
    }
}
