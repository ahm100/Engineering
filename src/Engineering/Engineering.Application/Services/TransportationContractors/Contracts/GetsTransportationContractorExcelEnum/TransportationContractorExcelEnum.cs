using System.ComponentModel;

namespace Engineering.Application.Services.TransportationContractors.Contracts.GetsTransportationContractorExcelEnum;

public enum TransportationContractorExcelEnum
{
    [Description("نام")] FirstName = 1,
    [Description("نام خانوادگی")] LastName = 2,
    [Description("نام و نام خانوادگی")] FullName = 3,
    [Description("شماره تماس")] PhoneNumber = 4,
    [Description("کدملی")] IdentityNo = 5,
    [Description("شروع قرارداد")] StartOfContractShamsi = 6,
    [Description("پایان قرارداد")] EndOfContractShamsi = 7,
    [Description("توضیحات")] Description = 8,
    [Description("وضعیت")] IsActive = 9,
    [Description("تاریخ ایجاد")] CreatedShamsi = 10,
    [Description("نام شرکت")] CompanyName = 11,
    [Description("شماره ثبت")] RegisterationNo = 12,
    [Description("شهر")] City = 13,
    [Description("آدرس")] Address = 14,
    [Description("عنوان آدرس")] Title = 15,
    [Description("کدپستی")] PostalCode = 16,
    [Description("ارزش افزوده(درصد مالیات)")] TaxPercent = 17,
    [Description("هزینه خدمات")] ServicePrice = 18,
}