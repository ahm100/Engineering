using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplies.Models.GetsRequestGoodsSupplyDetailExcelEnums;

public enum RequestGoodsSupplyDetailExcelEnum
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

    [Description("گروه کالا")]
    GroupName = 6,

    [Description("کد گروه کالا")]
    GroupCode = 7,

    [Description("واحد گروه")]
    GroupMeasure = 8,

    [Description("شناسه کالا")]
    ProductId = 9,

    [Description("کالا")]
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

    [Description("تعداد کل تخمین زده شده")]
    TotalEstimatedCount = 20,

    [Description("تعداد کل درخواست شده")]
    TotalRequestedCount = 21,

    [Description("تعداد کل تامین شده")]
    TotalSupplyCount = 22,

    [Description("تعداد باقی مانده")]
    TotalRemainedCount = 23,

    [Description("تعداد درخواست")]
    RequestedCount = 24,

    [Description("تعداد تامین")]
    SupplyCount = 25,

    [Description("تعداد باقی مانده")]
    RemainedCount = 26,

    [Description("تاریخ نهایی ارسال")]
    DelivaryDeadLine = 27,

    [Description("شناسه ارز")]
    CurrencyId = 28,

    [Description("ارز")]
    Currency = 29,

    [Description("قیمت نهایی")]
    TotalPrice = 30,

    [Description("الویت")]
    ImportanceDescription = 31,

    [Description("وضعیت")]
    StatusDescription = 32,

    [Description("ایجاد کننده")]
    Creator = 33,

    [Description("توضیحات")]
    Description = 34,

    [Description("توضیحات کارشناس ارشد")]
    ManagementDescription = 35,

    [Description("شناسه انبار مقصد")]
    DestinationWarehouseId = 36,

    [Description("انبار مقصد")]
    DestinationWarehouse = 37,

    [Description("شناسه پیمانکار")]
    ContractorId = 38,

    [Description("پیمانکار")]
    FullName = 39,

    [Description("شناسه بسته")]
    PackageId = 40,

    [Description("گنجایش بسته")]
    PackageQuantity = 41,

    [Description("عنوان بسته")]
    PackageTitle = 42,

    [Description("تعداد بسته")]
    PackageCount = 43,

    [Description("توضیحات ریزمتره")]
    ProjectOperationDetailDescription = 44,

    [Description("شماره فاکتور")]
    CustomerInvoiceNumber = 45,

    [Description("اخرین توضیحات")]
    LastDescription = 46
}