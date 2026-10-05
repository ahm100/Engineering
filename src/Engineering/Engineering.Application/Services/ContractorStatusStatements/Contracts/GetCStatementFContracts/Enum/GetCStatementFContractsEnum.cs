using System.ComponentModel;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts.Enum;

public enum GetCStatementFContractsExcelEnum
{
    [Description("لیست صورت وضعیت های پیمانکار")]
    Contracts = 1,

    [Description("لیست صورت وضعیت های خدمات روزانه")]
    DailyServices = 2
}

public enum GetCStatementFContractsEnum
{
    [DefaultHeader]
    [Description("ردیف")]
    Id = 1,

    [DefaultHeader]
    [Description("شناسه قرارداد")]
    ContractId = 2,

    [DefaultHeader]
    [Description("شناسه هدر قرارداد پیمانکار")]
    ContractorContractHeaderId = 3,

    [DefaultHeader]
    [Description("شرح قرارداد پیمانکار")]
    ContractorContractHeaderDescription = 4,

    [DefaultHeader]
    [Description("نوع قرارداد پیمانکار")]
    ContractorContractType = 5,

    [DefaultHeader]
    [Description("کد نوع قرارداد پیمانکار")]
    ContractorContractTypeCode = 6,

    [DefaultHeader]
    [Description("شناسه پروژه")]
    ProjectId = 7,

    [DefaultHeader]
    [Description("نام پروژه")]
    ProjectName = 8,

    [DefaultHeader]
    [Description("شناسه پیمانکار")]
    ContractorId = 9,

    [DefaultHeader]
    [Description("نام پیمانکار")]
    Contractor = 10,

    [DefaultHeader]
    [Description("تاریخ شروع")]
    StartDate = 11,

    [DefaultHeader]
    [Description("تاریخ پایان")]
    EndDate = 12,

    [DefaultHeader]
    [Description("مبلغ کل")]
    TotalAmount = 13,

    [DefaultHeader]
    [Description("درصد انجام کار مطلوب")]
    PercentageDoingJobWell = 14,

    [DefaultHeader]
    [Description("مبلغ انجام کار مطلوب")]
    DoingJobWellAmount = 15,

    [DefaultHeader]
    [Description("درصد پیش پرداخت")]
    PercentageAdvancePayment = 16,

    [DefaultHeader]
    [Description("مبلغ پیش پرداخت")]
    AdvancePaymentAmount = 17,

    [DefaultHeader]
    [Description("جریمه دیرکرد روزانه")]
    DailyLatenessPenalty = 18,

    [DefaultHeader]
    [Description("درصد قرارداد ثابت")]
    FixedContractPct = 19,

    [DefaultHeader]
    [Description("توضیحات درصد قرارداد ثابت")]
    FixedContractPctDesc = 20,

    [DefaultHeader]
    [Description("درصد قرارداد ثابت پروژه")]
    ProjectFixedContractPct = 21,

    [DefaultHeader]
    [Description("توضیحات درصد قرارداد ثابت پروژه")]
    ProjectFixedContractPctDesc = 22,

    [DefaultHeader]
    [Description("درصد قرارداد ثابت مدیر")]
    ManagerFixedContractPct = 23,

    [DefaultHeader]
    [Description("توضیحات درصد قرارداد ثابت مدیر")]
    ManagerFixedContractPctDesc = 24,

    [DefaultHeader]
    [Description("توضیحات")]
    Description = 25,
}

public enum GetCStatementFContractsDailiesEnum
{
    [DefaultHeader]
    [Description("ردیف")]
    Id = 1,

    [DefaultHeader]
    [Description("شناسه خدمت روزانه")]
    DailyServiceId = 2,

    [DefaultHeader]
    [Description("شناسه روزانه")]
    DailyId = 3,

    [DefaultHeader]
    [Description("حجم")]
    Volume = 4,

    [DefaultHeader]
    [Description("دارای مستندات")]
    HaveDocuments = 5,

    [DefaultHeader]
    [Description("تاریخ ایجاد")]
    Created = 6,

    [DefaultHeader]
    [Description("شناسه ایجاد کننده")]
    CreatorId = 7,

    [DefaultHeader]
    [Description("ایجاد کننده")]
    Creator = 8,

    [DefaultHeader]
    [Description("شناسه عملیات پروژه")]
    ProjectOperationId = 9,

    [DefaultHeader]
    [Description("مقدار کارکرد")]
    Workload = 10,

    [DefaultHeader]
    [Description("نام عملیات")]
    OperationInfoName = 11,

    [DefaultHeader]
    [Description("کد عملیات")]
    OperationInfoCode = 12,

    [DefaultHeader]
    [Description("شناسه واحد عملیات پروژه")]
    ProjectOperationMeasureId = 13,

    [DefaultHeader]
    [Description("واحد سنجش عملیات پروژه")]
    ProjectOperationMeasurement = 14,

    [DefaultHeader]
    [Description("شناسه خدمت پیمانکار")]
    ProjectOperationDetailContractorServiceId = 15,

    [DefaultHeader]
    [Description("شناسه خدمت")]
    ServiceInfoId = 16,

    [DefaultHeader]
    [Description("نام خدمت")]
    ServiceInfoName = 17,

    [DefaultHeader]
    [Description("کد خدمت")]
    ServiceInfoCode = 18,

    [DefaultHeader]
    [Description("شناسه واحد خدمت")]
    ServiceInfoMeasureId = 19,

    [DefaultHeader]
    [Description("واحد سنجش خدمت")]
    ServiceInfoMeasurement = 20,

    [DefaultHeader]
    [Description("حجم خدمت")]
    ServiceVolume = 21,

    [DefaultHeader]
    [Description("طول")]
    Length = 22,

    [DefaultHeader]
    [Description("عرض")]
    Width = 23,

    [DefaultHeader]
    [Description("ارتفاع")]
    Height = 24,

    [DefaultHeader]
    [Description("وزن")]
    Weight = 25,

    [DefaultHeader]
    [Description("تعداد")]
    Number = 26,

    [DefaultHeader]
    [Description("مبلغ نهایی")]
    FinalAmount = 27,

    [DefaultHeader]
    [Description("کد عمومی")]
    PublicCode = 28,

    [DefaultHeader]
    [Description("نام عمومی")]
    PublicName = 29,

    [DefaultHeader]
    [Description("توضیحات")]
    Description = 30,
}
