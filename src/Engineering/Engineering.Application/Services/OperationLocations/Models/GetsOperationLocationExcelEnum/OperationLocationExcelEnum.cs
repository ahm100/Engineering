using System.ComponentModel;

namespace Engineering.Application.Services.OperationLocations.Models.GetsOperationLocationExcelEnum;

public enum OperationLocationExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("نام عمومی")]
    PublicName = 2,

    [Description("نام خصوصی")]
    PrivateName = 3,

    [Description("مسیر")]
    Path = 4,

    [Description("کد عمومی")]
    PublicCode = 5,

    [Description("کد خصوصی")]
    PrivateCode = 6,

    [Description("اطلاعات مکان عملیات")]
    OperationLocationInfo = 7,

    [Description("شناسه والد")]
    ParentId = 8,

    [Description("نام عمومی والد")]
    ParentPublicName = 9,

    [Description("کد عمومی والد")]
    ParentPublicCode = 10,

    [Description("شناسه مرکزهزینه")]
    CostCenterId = 11,

    [Description("نام مرکزهزینه")]
    CostCenterName = 12,

    [Description("اولویت")]
    Priority = 13,

    [Description("وضعیت")]
    IsActive = 14,

    [Description("دارای فرزند")]
    HaveChild = 15,

    [Description("تعداد فرزند")]
    ChildCount = 16,

    [Description("شناسه موسسه")]
    CompanyId = 17,

    [Description("نام موسسه")]
    CompanyNameFa = 18,

    [Description("شناسه پروژه")]
    ProjectId = 19,

    [Description("نام پروژه")]
    ProjectName = 20
}