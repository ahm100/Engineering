
namespace Engineering.Domain.Entities.Messengers.Enums;

public enum MessengerMessageType
{
    [Description("Test")]
    Test = 0,

    [Description("حواله های انبار")]
    WareInvocies = 10,

    [Description("کارکرد روزانه")]
    DailyOperation = 12,

    [Description("تایید درخواست ترابری")]
    ConfirmTransportation = 14,

    [Description("پرداخت درخواست ترابری")]
    PaidTransportation = 16,

    [Description("پرداخت اسنپ")]
    PaidSnap = 18,

    [Description("درخواست ماشین آلات")]
    RequestMachinery = 20,

    [Description("درخواست تامین")]
    SupplyRequest = 22,

    [Description("تغییر وضعیت درخواست پشتیبانی انبار")]
    CommerceRequestWarehouseStatusChange = 24,

    [Description("ورود انبار")]
    WarehouseEntry = 26,

    [Description("بین انباری")]
    betweenStorage = 28,

    [Description("پرداخت")]
    Payment = 30,

    [Description("پکینگ")]
    Packing = 32,

    [Description("تایید دستور پرداخت")]
    AcceptPaymentOrder = 34,

    [Description(" پرداخت صورت وضعیت پیمانکار")]
    PaymentContractorStatusStatement = 36,

    [Description("تغییرات کاربر")]
    UserChanged = 38,

    [Description("ورود بین انباری برای تحویل موقت")]
    EntryThroughRelocationForTemporaryDelivery = 40,

    [Description("خروج بین انباری برای تحویل موقت")]
    ExitRelocationForTemporaryDelivery = 42,

    [Description("تمامی پیام ها")]
    Other = 100,
}
