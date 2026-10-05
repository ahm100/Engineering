using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractServiceReport.Enum;

public enum ContractorContractReportExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("نام مرکز هزینه")]
    CostCenterName = 2,
    [Description("نام پروژه")]
    ProjectName = 3,
    [Description("نام شرح عملیات")]
    ProjectOperationName = 4,
    [Description("وضعیت شرح عملیات")]
    ProjectOperationStatusDescription = 5,
    [Description("نام واحدشرح عملیات")]
    MeasurementName = 6,
    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 7,
    [Description("وضعیت ریزمتره")]
    ProjectOperationDetailStatusDescription = 8,
    [Description("تاریخ ریزمتره")]
    ProjectOperationDetailStartDateShamsi = 9,
    [Description("تاریخ پایان ریزمتره")]
    ProjectOperationDetailEndDateShamsi = 10,
    [Description("تاریخ ایجاد ریزمتره")]
    ProjectOperationDetailCreateDateShamsi = 11,
    [Description("نام ریزمتره")]
    PrivateName = 12,
    [Description("کد ریزمتره")]
    PrivateCode = 13,
    [Description("نام عمومی ریزمتره")]
    PublicName = 14,
    [Description("کد عمومی ریزمتره")]
    PublicCode = 15,
    [Description("نام خدمات")]
    ServiceInfoName = 16,
    [Description("کد خدمات")]
    ServiceInfoCode = 17,
    [Description("شناسه اندازه‌گیری خدمات")]
    ServiceInfoMeasureId = 18,
    [Description("پیمانکار")]
    Contractor = 19,
    [Description("نام مستعار پیمانکار")]
    ContractorNickName = 20,
    [Description("نام ایجادکننده")]
    CreatorName = 21,
    [Description("نام مستعار ایجادکننده")]
    CreatorNickname = 22,
    [Description("تاریخ ایجاد")]
    CreatedShamsi = 23,
    [Description("حجم")]
    Volume = 24,
    [Description("قیمت")]
    Price = 25,
    [Description("قیمت کل")]
    TotalPrice = 26,
    [Description("ارز")]
    Currency = 27,
    [Description("کد قرارداد پیمانکار")]
    ContractorContractCode = 28,
    [Description("وضعیت قرارداد پیمانکار")]
    ContractorContractStatusDescription = 29,
    [Description("نوع قرارداد پیمانکار")]
    ContractorContractType = 30,
    [Description("تاریخ شروع ")]
    StartDateShamsi = 31,
    [Description("تاریخ پایان ")]
    EndDateShamsi = 32,
    [Description("توضیحات")]
    Description = 33,
}