using System.ComponentModel;

namespace Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads.Enum
{
    public enum FltrEContractHeadsExcelEnum
    {
        [DefaultHeader]
        [Description("ردیف")]
        Id = 1,

        [DefaultHeader]
        [Description("کد قرارداد")]
        Code = 2,

        [DefaultHeader]
        [Description("کارفرما")]
        Employer = 3,

        [DefaultHeader]
        [Description("ارز")]
        Currency = 4,

        [DefaultHeader]
        [Description("مرکز هزینه")]
        CostCenterName = 5,

        [DefaultHeader]
        [Description("کد مرکز هزینه")]
        CostCenterCode = 6,

        [DefaultHeader]
        [Description("پروژه")]
        ProjectName = 7,

        [DefaultHeader]
        [Description("کد پروژه")]
        ProjectCode = 8,

        [DefaultHeader]
        [Description("نوع قرارداد")]
        TypeDescription = 9,

        [DefaultHeader]
        [Description("تاریخ شروع")]
        StartDateShamsi = 10,

        [DefaultHeader]
        [Description("تاریخ پایان")]
        EndDateShamsi = 11,

        [DefaultHeader]
        [Description("تلورانس حجم")]
        VolumeTolerance = 12,

        [DefaultHeader]
        [Description("تلورانس قیمت")]
        PriceTolerance = 13,

        [DefaultHeader]
        [Description("تاریخ ایجاد")]
        CreatedShamsi = 14,

        [DefaultHeader]
        [Description("ایجاد کننده")]
        Creator = 15
    }
}
