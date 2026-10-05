
namespace Engineering.Domain.Entities.TelegramChats.Enums;

public enum TelegramMessageType
{
    [Description("Test")]
    Test = 0,

    [Description("وضعیت آب و هوا")]
    WeatherCondition = 1,

    [Description("تردد افراد")]
    TrafficOfPeople = 11,

    [Description("رد درخواست تامین")]
    SupplyRequestRejection = 22,

    [Description("برگشت درخواست تامین")]
    SupplyRequestReturn = 33,

    [Description("پرداخت")]
    Payment = 44,

    [Description("درخواست ترابری")]
    TransportRequest = 55,

    [Description("کارکرد روزانه")]
    DailyOperation = 66,

    [Description("پکینگ")]
    Packing = 77,

    [Description("خروج مصرفی")]
    ConsumerExit = 88,

    [Description("بین انباری")]
    betweenStorage = 99,

    [Description("ورود انبار")]
    WarehouseEntry = 111,

    [Description("تحویل موقت")]
    TemporaryDelivery = 122,

    [Description("تایید دستور پرداخت")]
    AcceptPaymentOrder = 133,

    [Description("پرداخت خزانه داری")]
    PaymentTreasury = 144,

    [Description("درخواست ماشین آلات")]
    RequestMachinery = 155,

    [Description("پرداخت درخواست ترابری")]
    TransportationPaid = 165,

    [Description("خروج بین انباری برای تحویل موقت")]
    ExitRelocationForTemporaryDelivery = 175,

    [Description("ورود بین انباری برای تحویل موقت")]
    EntryThroughRelocationForTemporaryDelivery = 185,

    [Description("تغییر وضعیت درخواست پشتیبانی انبار")]
    CommerceRequestWarehouseStatusChange = 195,

    [Description(" پرداخت صورت وضعیت پیمانکار")]
    PaymentContractorStatusStatement = 196,

    [Description("پرداخت درخواست اسنپ")]
    SnapPaid = 205,


    [Description("تغییرات کاربر")]
    UserChanged = 206,

    [Description("ارسال پیام همه گروه ها")]
    OtherGroups = 1000,

    [Description("حواله های انبار")]
    WareInvocies = 500,

}
