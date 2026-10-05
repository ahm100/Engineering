using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailReportsExcelEnums;

public enum RequestGoodsSupplyDetailReportsExcelEnum
{
    [Description("ردیف")]
    Row = 0,

    [Description("شناسه")]
    Id = 1,

    [Description("شناسه درخواست تامین کالا")]
    RequestGoodsSupplyId = 2,

    [Description("شناسه احجام مصرفی کالا")]
    ConsumableVolumeProductId = 3,

    [Description("تعداد کالا")]
    ProductNumber = 4,

    [Description("شناسه گروه کالا")]
    GroupId = 5,

    [Description("نام گروه کالا")]
    GroupName = 6,

    [Description("کد گروه کالا")]
    GroupCode = 7,

    [Description("واحد گروه کالا")]
    GroupMeasure = 8,

    [Description("شناسه کالا")]
    ProductId = 9,

    [Description("نام کالا")]
    ProductName = 10,

    [Description("کد کالا")]
    ProductCode = 11,

    [Description("برند")]
    Brand = 12,

    [Description("مدل برند")]
    BrandModel = 13,

    [Description("عنوان خصوصی")]
    PrivateName = 14,

    [Description("کد خصوصی")]
    PrivateCode = 15,

    [Description("عنوان عمومی")]
    PublicName = 16,

    [Description("کد عمومی")]
    PublicCode = 17,

    [Description("درصد تلورانس")]
    TolerancePercentage = 18,

    [Description("تعداد تلورانس")]
    ToleranceCount = 19,

    [Description("تعداد کل تخمینی")]
    TotalEstimatedCount = 20,

    [Description("تعداد کل درخواست")]
    TotalRequestedCount = 21,

    [Description("تعداد کل تامین شده")]
    TotalSupplyCount = 22,

    [Description("تعداد کل باقی مانده")]
    TotalRemainedCount = 23,

    [Description("تعداد درخواست")]
    RequestedCount = 24,

    [Description("تعداد تامین")]
    SupplyCount = 25,

    [Description("تعداد باقی مانده")]
    RemainedCount = 26,

    [Description("تاریخ تحویل")]
    DelivaryDeadLine = 27,

    [Description("شناسه ارز")]
    CurrencyId = 28,

    [Description("ارز")]
    Currency = 29,

    [Description("قیمت کل")]
    TotalPrice = 30,

    [Description("الویت")]
    ImportanceDescription = 31,

    [Description("وضعیت")]
    StatusDescription = 32,

    [Description("نوع درخواست کالا")]
    ManagementTypeDescription = 33,

    [Description("درخواست دهنده")]
    Creator = 34,

    [Description("توضیحات")]
    Description = 35,

    [Description("توضیحات مدیر")]
    ManagementDescription = 36,

    [Description("بررسی گروهی")]
    CheckGroup = 37,

    [Description("شناسه انبار مقصد")]
    DestinationWarehouseId = 38,

    [Description("شناسه پیمانکار")]
    ContractorId = 39,

    [Description("پیمانکار")]
    FullName = 40,

    [Description("شناسه بسته بندی")]
    PackageId = 41,

    [Description("تعداد بسته بندی")]
    PackageQuantity = 42,

    [Description("بسته بندی")]
    PackageTitle = 43,

    [Description("تعداد بسته بسته بندی")]
    PackagePackageCount = 44,

    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 45,

    [Description("شماره فاکتور")]
    CustomerInvoiceNumber = 46,

    [Description("آخرین توضیحات")]
    LastDescription = 47
}