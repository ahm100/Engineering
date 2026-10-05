using System.ComponentModel;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsAirplaneExcelEnum;

public enum AirplaneExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("عنوان ترابری")]
    TransportationName = 2,
    [Description("نوع ترابری")]
    TransportationTypeDescription = 3,
    [Description("نوع سفر")]
    TripName = 4,
    [Description("مرکز هزینه ها")]
    CostCenterNames = 5,
    [Description("پروژه ها")]
    ProjectNames = 6,
    [Description("مبدا")]
    StartingCityName = 7,
    [Description("مقصد")]
    DestinationCityName = 8,
    [Description("نام مسافر")]
    Passenger = 9,
    [Description("نام مسافر غیر طرف حساب")]
    PassengerName = 10,
    [Description("تاریخ رفت")]
    StartDateShamsi = 11,
    [Description("تاریخ برگشت")]
    EndDateShamsi = 12,
    [Description("توضیحات")]
    Description = 13,
    [Description("شماره حساب")]
    AccountNumber = 14,
    [Description("شماره کارت")]
    CardNumber = 15,
    [Description("صاحب حساب")]
    TicketPayer = 16,
    [Description("ارز")]
    CurrencyUnitName = 17,
    [Description("وضعیت")]
    TransportationRequestStatusTitle = 18,
    [Description("آدرس مقصد")]
    DestinationAddress = 19,
    [Description("کرایه")]
    FareAmount = 20,
    [Description("گروه هزینه")]
    TransportationCostGroupTitle = 21,
    [Description("کد گروه هزینه")]
    TransportationCostGroupCode = 22,
    [Description("دسته بندی هزینه")]
    TransportationCostCategoryTitle = 23,
    [Description("کد دسته بندی هزینه")]
    TransportationCostCategoryCode = 24,
    [Description("ثبت کننده")]
    Creator = 25,
    [Description("شماره شبا")]
    Iban = 26,
    [Description("توضیحات مدیر")]
    ManagerDescription = 27,
    [Description("تایید/رد کننده")]
    ConfrimName = 28,
    [Description("تاریخ تایید/رد")]
    ConfrimDateShamsi = 29,
    [Description("شماره درخواست")]
    RequestNumber = 30,
}