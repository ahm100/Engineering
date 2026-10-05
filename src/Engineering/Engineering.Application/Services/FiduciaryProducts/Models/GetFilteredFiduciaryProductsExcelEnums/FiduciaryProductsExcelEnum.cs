using System.ComponentModel;

namespace Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProductsExcelEnums;

public enum FiduciaryProductsExcelEnum
{
    [Description("شماره درخواست")]
    RequestNumber = 1,
    [Description("مرکز هزینه")]
    CostCenterName = 2,
    [Description("پروژه")]
    ProjectName = 3,
    [Description("شرح عملیات")]
    OperationInfoName = 4,
    [Description("تاریخ درخواست")]
    CreatedShamsi = 5,
    [Description("طرف حساب")]
    ThirdParty = 6,
    [Description("توضیحات")]
    Description = 7,
    [Description("آخرین توضیحات")]
    LastDescription = 8,
    [Description("توضیحات وضعیت")]
    StatusDescription = 9,
    [Description("ثبت کننده")]
    Creator = 10,
    [Description("وضعیت")]
    StatusTitle = 11,
    [Description("نام موسسه")]
    CompanyNameFa = 12
}
public enum FiduciaryProductDetailsExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("شناسه درخواست امانی")]
    FiduciaryProductId = 2,
    [Description("نام کالا")]
    ProductName = 3,
    [Description("کد کالا")]
    ProductCode = 4,
    [Description("برند کالا")]
    ProductBrand = 5,
    [Description("مدل برند کالا")]
    ProductBrandModel = 6,
    [Description("تعداد درخواستی")]
    LoanCount = 7,
    [Description("تعداد روز درخواستی")]
    LoanDays = 8,
    [Description("تعداد روز تایید شده")]
    ConfirmedLoanDays = 9,
    [Description("واحد اندازه‌گیری")]
    MeasureunitName = 10,
    [Description("ارز")]
    CurrencyName = 11,
    [Description("جریمه روزانه درخواستی")]
    DailyLateFine = 12,
    [Description("جریمه روزانه تایید شده")]
    ConfirmedDailyLateFine = 13,
    [Description("توضیحات کالا")]
    ProductDescription = 14,
    [Description("آخرین توضیحات")]
    LastDescription = 15,
    [Description("توضیحات وضعیت")]
    StatusDescription = 16,
    [Description("وضعیت")]
    StatusTitle = 17,
    [Description("تاریخ ایجاد")]
    CreatedShamsi = 18,
    [Description("ثبت کننده")]
    Creator = 19
}