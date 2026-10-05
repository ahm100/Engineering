using System.ComponentModel;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsWarehouseTransportationExcelEnum;

public enum WarehouseTransportationExcelEnum
{
    [Description("شماره درخواست")]
    RequestNumber = 1,

    [Description("مرکز هزینه")]
    CostCenterNames = 2,

    [Description("پروژه‌ها")]
    ProjectNames = 3,

    [Description("وضعیت")]
    StatusTitle = 4,

    [Description("نام حمل")]
    TransportationName = 5,

    [Description("هزینه حمل")]
    TransferPrice = 6,

    [Description("شهر مبدا")]
    StartingCityName = 7,

    [Description("شهر مقصد")]
    DestinationCityName = 8,

    [Description("آدرس مبدا")]
    StartingCityAddress = 9,

    [Description("آدرس مقصد")]
    DestinationAddress = 10,

    [Description("توضیحات")]
    Description = 11,

    [Description("نوع ماشین")]
    MachineType = 12,

    [Description("نام راننده")]
    DriverName = 13,

    [Description("شماره پلاک")]
    NumberPlate = 14,

    [Description("شماره گواهینامه")]
    CertificateNumber = 15,

    [Description("شناسه شخص ثالث")]
    ThirdPartyId = 16,

    [Description("نام اصلی")]
    MainName = 17,

    [Description("شماره تماس پیمانکار")]
    ContractorPhoneNumber = 18,

    [Description("شماره بارنامه")]
    FreightNumber = 19,

    [Description("انبارها")]
    Warehouses = 20,

    [Description("اشخاص ثالث")]
    ThirdParties = 21,

    [Description("نام شخص ثالث")]
    ThirdPartiesName = 22,

    [Description("شماره بسته‌بندی")]
    PackingNumbers = 23,

    [Description("شماره پالت")]
    PalletNumbers = 24,

    [Description("فاکتورهای خروج")]
    ExitInvoices = 25,

    [Description("تاریخ ارسال (شمسی)")]
    PostageDateShamsi = 26,

    [Description("فوری")]
    IsUrgent = 27,

    [Description("ایجادکننده")]
    Creator = 28,

    [Description("تاریخ ایجاد (شمسی)")]
    CreatedShamsi = 29,

    [Description("وزن بار")]
    LoadWeight = 30,

    [Description("حجم")]
    Volume = 31,

    [Description("روش تحویل")]
    DeliveryMethodTitle = 32,

    [Description("نوع تحویل")]
    DeliveryTypeTitle = 33,

    [Description("تجمیع فاکتورها")]
    AggregateInvoices = 34,

    [Description("فاکتورها")]
    Invoices = 35,

    [Description("کانال‌ها")]
    Channels = 36,
}

public enum WarehouseTransportationPackingExcelEnum
{
    [Description("شماره درخواست ترابری")]
    RequestNumber = 1,

    [Description("انبار مبدا")]
    SourceWarehouse = 2,

    [Description("انبار مقصد")]
    Warehouse = 3,

    [Description("شخص ثالث")]
    ThirdParty = 4,

    [Description("نام شخص ثالث")]
    ThirdPartyName = 5,

    [Description("کد یکتای شخص ثالث")]
    ThirdPartyUniqeCode = 6,

    [Description("شماره بسته‌بندی")]
    PackingNumber = 7,

    [Description("قیمت")]
    Price = 8,

    [Description("هزینه حمل")]
    TransferPrice = 9,

    [Description("شماره پالت")]
    PalletNumber = 10,

    [Description("مشخصات بسته‌بندی")]
    PackagingSpec = 11,

    [Description("نوع بسته")]
    Package = 12,

    [Description("تعداد")]
    Quantity = 13,

    [Description("وزن")]
    Weight = 14,

    [Description("تاریخ تحویل (شمسی)")]
    DeliveryDateShamsi = 15,

    [Description("هزینه ارسال")]
    ShippingCost = 16,

    [Description("فاکتور خروج")]
    ExitInvoice = 17,

    [Description("فاکتورها")]
    Invoices = 18,

    [Description("کانال")]
    Channel = 19,

    [Description("ارتفاع")]
    Height = 20,

    [Description("عرض")]
    Width = 21,

    [Description("طول")]
    Length = 22,

    [Description("حجم بسته‌بندی")]
    PackagingVolume = 23,
}

