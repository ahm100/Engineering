using System.ComponentModel;


namespace Engineering.Application.Services.ContractorStatusStatements.Contracts.GetIntegratedCSS.ExcelEnum
{
    public enum GetsIntegratedCSSEnum
    {
        [Description("قرارداد پیمانکار")]
        CostCenterName = 1,

        [Description("نوع قرارداد پیمانکار")]
        Project = 2,

        [Description("نام عملیات")]
        Contractor = 3,

        [Description("کد عملیات")]
        CreatorConfirmedAmount = 4,

        [Description("واحد شرح عملیات")]
        ProjectManagerConfirmedAmount = 5,

        [Description("حجم شرح عملیات")]
        ManagementConfirmedAmount = 6,

        [Description("نام خدمت")]
        ServiceInfoName = 7,

        [Description("کد خدمت")]
        ServiceInfoCode = 8,

        [Description("واحد خدمت")]
        ServiceInfoUnitOfMeasurement = 9,

        [Description("ریزمتره")]
        PublicName = 10,

        [Description("کد ریزمتره")]
        PublicCode = 11,

        [Description("توضیحات ریزمتره")]
        Description = 12,

        [Description("حجم ریزمتره")]
        FinalAmount = 13,

        [Description("تاریخ ایجاد")]
        Created = 14,

        [Description("قیمت پایه")]
        UnitPrice = 15,

        [Description("قیمت کل")]
        TotalPrice = 16,

        [Description("ایجادکننده")]
        Creator = 17,

        [Description("درصد قابل قبول کاربر")]
        AcceptablePercentage = 18,

        [Description("مقدار قابل قبول کاربر")]
        AcceptableAmount = 19,

        [Description("توضیح قابل قبول کاربر")]
        AcceptableDescription = 20,

        [Description("درصد قابل قبول مدیر پروژه")]
        ProjectManagementApprovalPercentage = 21,

        [Description("مقدار قابل قبول مدیر پروژه")]
        ProjectManagementApprovedPrice = 22,

        [Description("توضیحات مدیر پروژه")]
        ProjectManagementApprovedDescription = 23,

        [Description("درصد کارشناس ارشد")]
        ManagementApprovalPercentage = 24,

        [Description("مقدار قبول کارشناس ارشد")]
        ApprovedPrice = 25,

        [Description("توضیحات کارشناس ارشد")]
        ApprovedDescription = 26
    }
}
