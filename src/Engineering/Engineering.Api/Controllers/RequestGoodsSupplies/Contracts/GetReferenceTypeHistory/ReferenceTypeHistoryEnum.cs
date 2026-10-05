using System.ComponentModel;

namespace Engineering.Api.Controllers.RequestGoodsSupplies.Contracts.GetReferenceTypeHistory;

public enum ReferenceTypeHistoryEnum
{
    [Description("شناسه")]
    Id = 1,

    [Description("اهمیت")]
    Importance,

    [Description("شناسه مرجع")]
    ReferenceId,

    [Description("نام مرجع")]
    ReferenceName,

    [Description("نام پروژه")]
    ProjectName,

    [Description("کد پروژه")]
    ProjectCode,

    [Description("نام انگلیسی پروژه")]
    ProjectEnName,

    [Description("نوع تأمین")]
    Type,

    [Description("تعداد درخواستی")]
    RequestedCount,

    [Description("مهلت تحویل")]
    DelivaryDeadLine,

    [Description("مهلت تحویل شمسی")]
    DelivaryDeadLineShamsi,

    [Description("قیمت واحد")]
    UnitPrice,

    [Description("قیمت کل")]
    TotalPrice,

    [Description("هزینه بسته‌بندی")]
    PackingPrice,

    [Description("قیمت نهایی")]
    FinalPrice,

    [Description("توضیحات")]
    Description,

    [Description("توضیحات مدیریت")]
    ManagementDescription,

    [Description("شناسه پیمانکار")]
    ContractorId,

    [Description("پیمانکار")]
    Contractor,

    [Description("شناسه بسته")]
    PackageId,

    [Description("بسته")]
    Package,

    [Description("تعداد بسته")]
    PackageCount,

    [Description("قیمت واحد بسته")]
    PackageUnitPrice,

    [Description("آخرین توضیحات")]
    LastDescription
}