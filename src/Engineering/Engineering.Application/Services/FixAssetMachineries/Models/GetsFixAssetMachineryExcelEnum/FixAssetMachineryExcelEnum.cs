using System.ComponentModel;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryExcelEnum;

public enum FixAssetMachineryExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("نام ماشین آلات")]
    MachineryName = 2,
    [Description("کد ماشین آلات")]
    MachineryCode = 3,
    [Description("نوع موجودی")]
    TypeDesctiption = 4,
    [Description("نام راننده")]
    DriverFullname = 5,
    [Description("تاریخ شروع")]
    StartDateShamsi = 6,
    [Description("تاریخ پایان")]
    EndDateShamsi = 7,
    [Description("توضیحات")]
    Description = 8,
    [Description("مشخصات ماشین آلات")]
    MachinerySpecification = 9,
    [Description("پلاک")]
    NumberPlates = 10,
    [Description("قیمت ماشین آلات")]
    MachineryPrice = 11,
    [Description("نرخ ساعتی")]
    HourlyRate = 12,
    [Description("نرخ روزانه")]
    DailyRate = 13,
    [Description("نرخ سرویسی")]
    ServiceRate = 14,
    [Description("پیمانکار")]
    Contractor = 15,
    [Description("وضعیت")]
    IsActive = 16,
    [Description("کمپانی")]
    CompanyName = 17,
}