using System.ComponentModel;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts.Enum
{
    public enum FltrEContractsEnum
    {
        [DefaultHeader]
        [Description("ردیف")]
        Id = 1,

        [DefaultHeader]
        [Description("کد کامل قرارداد")]
        FullCode = 2,

        [DefaultHeader]
        [Description("شناسه سربرگ قرارداد")]
        HeadId = 3,

        [DefaultHeader]
        [Description("کد سربرگ قرارداد")]
        HeadCode = 4,

        [DefaultHeader]
        [Description("کد قرارداد")]
        Code = 5,

        [DefaultHeader]
        [Description("کارفرما")]
        Employer = 6,

        [DefaultHeader]
        [Description("ارز")]
        Currency = 7,

        [DefaultHeader]
        [Description("نرخ ارز")]
        CurrencyRate = 8,

        [DefaultHeader]
        [Description("مرکز هزینه")]
        CostCenterName = 9,

        [DefaultHeader]
        [Description("کد مرکز هزینه")]
        CostCenterCode = 10,

        [DefaultHeader]
        [Description("پروژه")]
        ProjectName = 11,

        [DefaultHeader]
        [Description("کد پروژه")]
        ProjectCode = 12,

        [DefaultHeader]
        [Description("اولین قرارداد")]
        IsFirst = 13,

        [DefaultHeader]
        [Description("وضعیت قرارداد")]
        StatusDescription = 14,

        [DefaultHeader]
        [Description("نوع قرارداد")]
        TypeDescription = 15,

        [DefaultHeader]
        [Description("تاریخ شروع")]
        StartDateShamsi = 16,

        [DefaultHeader]
        [Description("تاریخ پایان")]
        EndDateShamsi = 17,

        [DefaultHeader]
        [Description("مبلغ کل")]
        TotalAmount = 18,

        [DefaultHeader]
        [Description("پیش پرداخت")]
        AdvancePayment = 19,

        [DefaultHeader]
        [Description("توضیحات")]
        Description = 20,

        [DefaultHeader]
        [Description("تاریخ ایجاد")]
        CreatedShamsi = 21,

        [DefaultHeader]
        [Description("ایجاد کننده")]
        Creator = 22
    }
}
