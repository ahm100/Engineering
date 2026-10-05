using System.ComponentModel;

namespace Engineering.Application.Services.ShippingCosts.Contracts.GetsShippingCostExcelEnum;

public enum ShippingCostExcelEnum
{
    [Description("پیمانکار حمل")] MainName = 1,
    [Description("شماره تماس")] PhoneNumber = 2,
    [Description("شهر")] City = 3,
    [Description("آدرس")] Address = 4,
    [Description("عنوان آدرس")] Title = 5,
    [Description("کد پستی")] PostalCode = 6,
    [Description("ماشین")] MachinTypeName = 7,
    [Description("کد ماشین")] MachinTypeCode = 8,
    [Description("شهر مبدا")] SourceCity = 9,
    [Description("شهر مقصد")] DestinationCity = 10,
    [Description("ناحیه")] Region = 11,
    [Description("تعداد")] Count = 12,
    [Description("وزن")] LoadWeight = 13,
    [Description("قیمت")] Price = 14,
    [Description("مالیات")] Tax = 15,
    [Description("وضعیت")] IsActive = 16,
    [Description("توضیحات")] Description = 17,
    [Description("تاریخ شمسی")] CreatedShamsi = 18,
    [Description("طرف حساب")] ThirdParty = 19,
    [Description("طول جغرافیایی")] Longitude = 20,
    [Description("عرض جغرافیایی")] Latitude = 21,
}