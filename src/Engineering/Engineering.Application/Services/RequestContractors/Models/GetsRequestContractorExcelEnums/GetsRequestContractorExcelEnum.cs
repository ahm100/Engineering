using System.ComponentModel;

namespace Engineering.Application.Services.RequestContractors.Models.GetsRequestContractorExcelEnums;

public enum GetsRequestContractorExcelEnum
{
    [Description("شناسه درخواست پیمانکار")] RequestContractorId = 1,
    [Description("توضیحات وضعیت")] StatusDescription = 2,
    [Description("شماره درخواست")] RequestNumber = 3,
    [Description("نام مرکز هزینه")] CostCenterName = 4,
    [Description("نام پروژه")] ProjectName = 5,
    [Description("نام عملیات")] OperationInfoName = 6,
    [Description("کد عملیات")] OperationInfoCode = 7,
    [Description("نام ریزمتره")] PublicName = 8,
    [Description("کد ریزمتره")] PublicCode = 9,
    [Description("نام سرویس")] ServiceInfoName = 10,
    [Description("کد سرویس")] ServiceInfoCode = 11,
    [Description("حجم")] Volume = 12,
    [Description("توضیحات")] Description = 13,
    [Description("وضعیت توضیحات")] DescriptionStatus = 14,
    [Description("ایجادکننده")] Creator = 15,
    [Description("تاریخ ایجاد")] Created = 16,
    [Description("نام پیمانکار")] ContractorName = 17,
    [Description("نام مستعار پیمانکار")] ContractorNickName = 18,
    [Description("مبلغ کل")] TotalAmount = 19,
    [Description("مبلغ")] Amount = 20,
    [Description("تخفیف")] Discount = 21,
    [Description("مالیات")] Tax = 22,
    [Description("نام ارز")] CurrencyName = 23,
    [Description("توضیحات نوع")] TypeDescription = 24,
    [Description("تاریخ شروع")] FromDate = 25,
    [Description("تاریخ پایان")] ToDate = 26,
    [Description("نام تایید کننده")] ConfirmUserName = 27
}
