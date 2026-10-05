namespace Engineering.Application.Services.TransportationRequests.Models.GetsAggregateWarehouseTransportationExcelEnum;

using System.ComponentModel;
public enum AggregateWarehouseTransportationExcelEnum
{
    [Description("شماره درخواست")]
    RequestNumber = 1,

    [Description("مراکز هزینه")]
    CostCenterNames = 2,

    [Description("پروژه‌ها")]
    ProjectNames = 3,

    [Description("وضعیت")]
    StatusTitle = 4,

    [Description("نام حمل‌ونقل")]
    TransportationName = 5,

    [Description("هزینه حمل")]
    TransferPrice = 6,

    [Description("شهر مبدأ")]
    StartingCityName = 7,

    [Description("شهر مقصد")]
    DestinationCityName = 8,

    [Description("آدرس مبدأ")]
    StartingCityAddress = 9,

    [Description("آدرس مقصد")]
    DestinationAddress = 10,

    [Description("توضیحات")]
    Description = 11,

    [Description("نوع ماشین")]
    MachineType = 12,

    [Description("نام راننده")]
    DriverName = 13,

    [Description("شماره تماس راننده")]
    DriverPhoneNumber = 14,

    [Description("شماره گیربکس")]
    GearBoxNumber = 15,

    [Description("کد اصلی")]
    MainCode = 16,

    [Description("آدرس اصلی")]
    MainAddress = 17,

    [Description("شماره گواهینامه")]
    CertificateNumber = 18,

    [Description("پلاک")]
    NumberPlate = 19,

    [Description("شناسه پیمانکار")]
    TransportationContractorId = 20,

    [Description("نوع محاسبه")]
    CalculateTypeTitle = 21,

    [Description("شماره بارنامه")]
    FreightNumber = 22,

    [Description("انبارها")]
    Warehouses = 23,

    [Description("اشخاص ثالث")]
    ThirdParties = 24,

    [Description("نام اشخاص ثالث")]
    ThirdPartiesName = 25,

    [Description("شماره‌های بسته‌بندی")]
    PackingNumbers = 26,

    [Description("شماره‌های پالت")]
    PalletNumbers = 27,

    [Description("فاکتورهای خروج")]
    ExitInvoices = 28,

    [Description("آگهی")]
    IsAggregate = 29,

    [Description("تاریخ پست")]
    PostageDateShamsi = 30,

    [Description("فوری")]
    IsUrgent = 31,

    [Description("ایجادکننده")]
    Creator = 32,

    [Description("تاریخ ایجاد")]
    CreatedShamsi = 33,

    [Description("وزن بار")]
    LoadWeight = 34,

    [Description("حجم")]
    Volume = 35,

    [Description("روش تحویل")]
    DeliveryMethodTitle = 36,

    [Description("نوع تحویل")]
    DeliveryTypeTitle = 37,

    [Description("فاکتورهای تجمیعی")]
    AggregateInvoices = 38,

    [Description("فاکتورها")]
    Invoices = 39,

    [Description("کانال‌ها")]
    Channels = 40,

    [Description("بارنامه سراسری")]
    GlobalFreightNumber = 41,

    [Description("بارنامه طبقه‌بندی")]
    ClassifiedFreightNumber = 42,

    [Description("مالیات")]
    Tax = 43,

    [Description("هزینه حمل اضافی")]
    ExtraTransferPrice = 44,

    [Description("هزینه خدمات")]
    ServicePrice = 45,

    [Description("شماره بیمه")]
    InsuranceNumber = 46,

    [Description("هزینه بیمه")]
    InsurancePrice = 47,

    [Description("هزینه حمل دریایی")]
    ShippingCost = 48,

    [Description("قیمت کل محصول")]
    ProductTotalPrice = 49,

    [Description("خارج از محدوده")]
    OutofRange = 50,

    [Description("شماره سفارش")]
    OrderNumber = 51
}

public enum AggregateWarehouseTransportationPackingExcelEnum
{
    [Description("انبار")]
    Warehouse = 1,

    [Description("طرف حساب")]
    ThirdParty = 2,

    [Description("نام طرف حساب")]
    ThirdPartyName = 3,

    [Description("کد یکتای طرف حساب")]
    ThirdPartyUniqeCode = 4,

    [Description("شماره بسته‌بندی")]
    PackingNumber = 5,

    [Description("قیمت")]
    Price = 6,

    [Description("هزینه حمل")]
    TransferPrice = 7,

    [Description("شماره پالت")]
    PalletNumber = 8,

    [Description("مشخصات بسته‌بندی")]
    PackagingSpec = 9,

    [Description("بسته")]
    Package = 10,

    [Description("تعداد")]
    Quantity = 11,

    [Description("وزن")]
    Weight = 12,

    [Description("تاریخ تحویل")]
    DeliveryDateShamsi = 13,

    [Description("هزینه حمل")]
    ShippingCost = 14,

    [Description("فاکتور خروج")]
    ExitInvoice = 15,

    [Description("فاکتورها")]
    Invoices = 16,

    [Description("کانال")]
    Channel = 17,

    [Description("ارتفاع")]
    Height = 18,

    [Description("عرض")]
    Width = 19,

    [Description("طول")]
    Length = 20,

    [Description("حجم بسته‌بندی")]
    PackagingVolume = 21
}
