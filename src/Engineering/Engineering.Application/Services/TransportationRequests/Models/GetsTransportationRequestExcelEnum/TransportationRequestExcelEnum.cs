using System.ComponentModel;

namespace Engineering.Application.Services.TransportationRequests.Models.GetsTransportationRequestExcelEnum;

public enum TransportationRequestExcelEnum
{
    [Description("شناسه")]
    Id = 1,
    [Description("وضعیت")]
    TransportationRequestStatusTitle = 2,
    [Description("تاریخ شروع")]
    StartDate = 3,
    [Description("تاریخ پایان")]
    EndDate = 4,
    [Description("نوع ترابری")]
    TransportationName = 5,
    [Description("مسافری")]
    IsPassenger = 6,
    [Description("نوع سفر")]
    TripName = 7,
    [Description("نوع بارنامه")]
    BillOfLadingName = 8,
    [Description("شناسه درخواست دهنده")]
    RequestById = 9,
    [Description("درخواست دهنده")]
    RequestByName = 10,
    [Description("مبلغ")]
    Price = 11,
    [Description("شناسه واحد ارزی")]
    CurrencyUnitId = 12,
    [Description("واحد ارزی")]
    CurrencyUnitName = 13,
    [Description("توضیحات مدیر")]
    ManagerDescription = 14,
    [Description("شناسه مبدا")]
    StartingCityId = 15,
    [Description("مبدا")]
    StartingCityName = 16,
    [Description("شناسه مقصد")]
    DestinationCityId = 17,
    [Description("مقصد")]
    DestinationCityName = 18,
    [Description("مرکزهزینه ها")]
    CostCenterNames = 19,
    [Description("پروژه ها")]
    ProjectNames = 20,
    [Description("شناسه تایید/رد کننده")]
    ConfirmUserId = 21,
    [Description("تایید/رد کننده")]
    ConfirmUser = 22,
    [Description("تاریخ تایید/رد")]
    ConfirmDateShamsi = 23,
    [Description("توضیحات")]
    Description = 24,
    [Description("شناسه موسسه")]
    CompanyId = 25,
    [Description("نام موسسه")]
    CompanyNameFa = 26,
    [Description("گروه هزینه")]
    TransportationCostGroupTitle = 27,
    [Description("کد گروه هزینه")]
    TransportationCostGroupCode = 28,
    [Description("دسته بندی هزینه")]
    TransportationCostCategoryTitle = 29,
    [Description("کد دسته بندی هزینه")]
    TransportationCostCategoryCode = 30,
    [Description("آدرس مبدا")]
    StartingCityAddress = 31,
    [Description("آدرس مقصد")]
    DestinationAddress = 32,
    [Description("شماره درخواست")]
    RequestNumber = 33,
    [Description("نوع پرداخت")]
    PaymentType = 34,
    [Description("تاریخ پرداخت")]
    PaymentDateShamsi = 35,
    [Description("درخواست پنل هواپیمایی")]
    IsAirPlane = 36,
    [Description("پرداخت کننده بلیط")]
    TicketPayer = 37,
    [Description("نام پیمانکار حمل")]
    MainName = 38,
    [Description("شماره تماس پیمانکار")]
    ContractorPhoneNumber = 39,
    [Description("شهر")]
    City = 40,
    [Description("آدرس")]
    Address = 41,
    [Description("عنوان آدرس")]
    Title = 42,
    [Description("کدپستی")]
    PostalCode = 43,
}