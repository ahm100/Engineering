using System.ComponentModel;

namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts.Enum
{
    public enum GetCStatementSContractsExcelEnum
    {
        [Description("لیست صورت وضعیت های خدمات پیمانکار")]
        CSSContracts = 1,

        [Description("لیست صورت وضعیت های خدمات روزانه")]
        CSSServices = 2
    }

    public enum GetCStatementSContractsEnum
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
        [Description("مبلغ کار انجام شده")]
        TotalWorkedAmount = 14,

        [DefaultHeader]
        [Description("درصد انجام کار مطلوب")]
        PercentageDoingJobWell = 15,

        [DefaultHeader]
        [Description("مبلغ انجام کار مطلوب")]
        DoingJobWellAmount = 16,

        [DefaultHeader]
        [Description("درصد پیش پرداخت")]
        PercentageAdvancePayment = 17,

        [DefaultHeader]
        [Description("مبلغ پیش پرداخت")]
        AdvancePaymentAmount = 18,

        [DefaultHeader]
        [Description("جریمه دیرکرد روزانه")]
        DailyLatenessPenalty = 19,

        [DefaultHeader]
        [Description("توضیحات")]
        Description = 20,
    }

    public enum GetCStatementSContractsDailiesEnum
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
        [Description("قیمت واحد")]
        UnitPrice = 5,

        [DefaultHeader]
        [Description("مبلغ کل")]
        TotalPrice = 6,

        [DefaultHeader]
        [Description("درصد قابل قبول")]
        AcceptablePercentage = 7,

        [DefaultHeader]
        [Description("مبلغ قابل قبول")]
        AcceptableAmount = 8,

        [DefaultHeader]
        [Description("توضیحات قابل قبول")]
        AcceptableDescription = 9,

        [DefaultHeader]
        [Description("درصد تایید مدیریت پروژه")]
        ProjectManagementApprovalPercentage = 10,

        [DefaultHeader]
        [Description("مبلغ تایید شده مدیریت پروژه")]
        ProjectManagementApprovedPrice = 11,

        [DefaultHeader]
        [Description("توضیحات تایید مدیریت پروژه")]
        ProjectManagementApprovedDescription = 12,

        [DefaultHeader]
        [Description("درصد تایید مدیر")]
        ManagementApprovalPercentage = 13,

        [DefaultHeader]
        [Description("مبلغ تایید شده نهایی")]
        ApprovedPrice = 14,

        [DefaultHeader]
        [Description("توضیحات تایید نهایی")]
        ApprovedDescription = 15,

        [DefaultHeader]
        [Description("دارای مستندات")]
        HaveDocuments = 16,

        [DefaultHeader]
        [Description("تاریخ ایجاد")]
        Created = 17,

        [DefaultHeader]
        [Description("شناسه ایجاد کننده")]
        CreatorId = 18,

        [DefaultHeader]
        [Description("ایجاد کننده")]
        Creator = 19,

        [DefaultHeader]
        [Description("شناسه عملیات پروژه")]
        ProjectOperationId = 20,

        [DefaultHeader]
        [Description("مقدار کارکرد")]
        Workload = 21,

        [DefaultHeader]
        [Description("نام عملیات")]
        OperationInfoName = 22,

        [DefaultHeader]
        [Description("کد عملیات")]
        OperationInfoCode = 23,

        [DefaultHeader]
        [Description("شناسه واحد عملیات پروژه")]
        ProjectOperationMeasureId = 24,

        [DefaultHeader]
        [Description("واحد سنجش عملیات پروژه")]
        ProjectOperationMeasurement = 25,

        [DefaultHeader]
        [Description("شناسه خدمت پیمانکار")]
        ProjectOperationDetailContractorServiceId = 26,

        [DefaultHeader]
        [Description("شناسه خدمت")]
        ServiceInfoId = 27,

        [DefaultHeader]
        [Description("نام خدمت")]
        ServiceInfoName = 28,

        [DefaultHeader]
        [Description("کد خدمت")]
        ServiceInfoCode = 29,

        [DefaultHeader]
        [Description("شناسه واحد خدمت")]
        ServiceInfoMeasureId = 30,

        [DefaultHeader]
        [Description("واحد سنجش خدمت")]
        ServiceInfoMeasurement = 31,

        [DefaultHeader]
        [Description("حجم خدمت")]
        ServiceVolume = 32,

        [DefaultHeader]
        [Description("طول")]
        Length = 33,

        [DefaultHeader]
        [Description("عرض")]
        Width = 34,

        [DefaultHeader]
        [Description("ارتفاع")]
        Height = 35,

        [DefaultHeader]
        [Description("وزن")]
        Weight = 36,

        [DefaultHeader]
        [Description("تعداد")]
        Number = 37,

        [DefaultHeader]
        [Description("مبلغ نهایی")]
        FinalAmount = 38,

        [DefaultHeader]
        [Description("کد عمومی")]
        PublicCode = 39,

        [DefaultHeader]
        [Description("نام عمومی")]
        PublicName = 40,

        [DefaultHeader]
        [Description("توضیحات")]
        Description = 41,
    }
}
