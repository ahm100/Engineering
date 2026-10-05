using System.ComponentModel;

namespace Engineering.Application.Services.RequestGoodsSupplyDetails.Models.GetsRequestGoodsSupplyProductExcelEnums;

public enum RequestGoodsSupplyProductExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("شناسه درخواست")]
    RequestGoodsSupplyId = 2,
    [Description("شماره سریال")]
    SerialNumber = 3,
    [Description("شماره درخواست")]
    RequestNumber = 4,
    [Description("مرکز هزینه")]
    CostCenterName = 5,
    [Description("پروژه")]
    ProjectName = 6,
    [Description("مدیر پروژه")]
    ProjectManager = 7,
    [Description("شرح عملیات")]
    ProjectOperationName = 8,
    [Description("کد شرح عملیات")]
    ProjectOperationCode = 9,
    [Description("واحد شرح عملیات")]
    MeasurementName = 10,
    [Description("حجم شرح عملیات")]
    Workload = 11,
    [Description("ریزمتره ها(موقعیت های جزیی)")]
    ProjectOperationDetailNames = 12,
    [Description("کد ریزمتره ها")]
    ProjectOperationDetailCodes = 13,
    [Description("درجه اهمیت")]
    ImportanceDescription = 14,
    [Description("وضعیت")]
    StatusDescription = 15,
    [Description("نوع درخواست")]
    TypeDescription = 16,
    [Description("کالا")]
    ProductName = 17,
    [Description("کد کالا")]
    ProductCode = 18,
    [Description("برند کالا")]
    ProductBrand = 19,
    [Description("برند مدل کالا")]
    ProductBrandModel = 20,
    [Description("گروه کالا")]
    ProductGroupName = 21,
    [Description("کد گروه کالا")]
    ProductGroupCode = 22,
    [Description("تعداد درخواست")]
    RequestedCount = 23,
    [Description("تعداد تامین شده")]
    SupplyCount = 24,
    [Description("تعداد باقی مانده")]
    RemainedCount = 25,
    [Description("تاریخ آخرین موعد تحویل")]
    DelivaryDeadLine = 26,
    [Description("تاریخ درخواست")]
    RequestedDate = 27,
    [Description("تاریخ ثبت درخواست")]
    Created = 28,
    [Description("قیمت واحد")]
    UnitPrice = 29,
    [Description("قیمت کل")]
    TotalPrice = 30,
    [Description("درصد مالیات")]
    TaxPercentage = 31,
    [Description("شماره مالیات")]
    TaxNumber = 32,
    [Description("تخفیف به درصد")]
    DiscountByPercentage = 33,
    [Description("تخفیف به عدد")]
    DiscountByNumber = 34,
    [Description("مبلغ تخفیف")]
    DiscountedPrice = 35,
    [Description("هزینه بسته بندی")]
    PackingPrice = 36,
    [Description("هزینه ترابری")]
    TransferPrice = 37,
    [Description("هزینه متفرقه")]
    OtherPrice = 38,
    [Description("قیمت نهایی")]
    FinalPrice = 39,
    [Description("پیمانکار")]
    ContractorFullName = 40,
    [Description("تامین کننده")]
    SupplyerFullName = 41,
    [Description("ارز")]
    CurrencyName = 42,
    [Description("انبار مقصد")]
    DestinationWarehouseName = 43,
    [Description("شماره فاکتور مشتری")]
    CustomerInvoiceNumber = 44,
    [Description("ثبت کننده")]
    Creator = 45,
    [Description("توضیحات")]
    Description = 46,
    [Description("توضیحات مدیر")]
    ManagementDescription = 47,
    [Description("آخرین توضیحات")]
    LastDescription = 48,
    [Description("نام بسته")]
    PackageName = 49,
    [Description("گنجایش بسته")]
    PackageQuantity = 50,
    [Description("تعداد بسته")]
    PackageCount = 51,
    [Description("قیمت واحد بسته")]
    PackageUnitPrice = 52,
    [Description("متصدی")]
    OperatorAppointmentName = 53,
    [Description("توضیحات ریزمتره ها")]
    ProjectOperationDetailDescriptions = 54,
}