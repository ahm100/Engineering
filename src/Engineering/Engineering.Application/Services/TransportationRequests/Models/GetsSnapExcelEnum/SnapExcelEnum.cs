using System.ComponentModel;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsSnapExcelEnum;

public enum SnapExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("وضعیت")]
    TransportationRequestStatusTitle = 2,
    [Description("ساعت شروع")]
    StartTime = 3,
    [Description("ساعت پایان")]
    EndTime = 4,
    [Description("تاریخ شروع")]
    StartDateShamsi = 5,
    [Description("تاریخ پایان")]
    EndDateShamsi = 6,
    [Description("نوع ترابری")]
    TransportationName = 7,
    [Description("نوع سفر")]
    TripName = 8,
    [Description("درخواست دهنده اسنپ")]
    SnapRequesterName = 9,
    [Description("ایجاد کننده")]
    Creator = 10,
    [Description("ارز")]
    CurrencyUnitName = 11,
    [Description("مبدا")]
    StartingCityName = 12,
    [Description("آدرس مبدا")]
    StartingCityAddress = 13,
    [Description("مقصد")]
    DestinationCityName = 14,
    [Description("آدرس مقصد")]
    DestinationAddress = 15,
    [Description("مقصد دوم")]
    SecondDestinationCityName = 16,
    [Description("آدرس مقصد دوم")]
    SecondDestinationAddress = 17,
    [Description("مرکز هزینه ها")]
    CostCenterNames = 18,
    [Description("پروژه ها")]
    ProjectNames = 19,
    [Description("توضیحات")]
    Description = 20,
    [Description("مبلغ کرایه")]
    FareAmount = 21,
    [Description("میزان توقف")]
    StopRate = 22,
    [Description("پرداخت شخصی")]
    PersonalPayment = 23,
    [Description("رفت و برگشت دارد")]
    ReturnToStart = 24,
    [Description("گروه هزینه ترابری")]
    TransportationCostGroupTitle = 25,
    [Description("کد گروه هزینه ترابری")]
    TransportationCostGroupCode = 26,
    [Description("دسته بندی هزینه ترابری")]
    TransportationCostCategoryTitle = 27,
    [Description("کد دسته بندی هزینه ترابری")]
    TransportationCostCategoryCode = 28,
    [Description("توضیحات مدیر")]
    ManagerDescription = 29,
    [Description("تایید/رد کننده")]
    ConfrimName = 30,
    [Description("تاریخ تایید/رد")]
    ConfrimDateShamsi = 31,
    [Description("تحویل گیرنده")]
    RecipientName = 32,
    [Description("شماره درخواست")]
    RequestNumber = 33,
}