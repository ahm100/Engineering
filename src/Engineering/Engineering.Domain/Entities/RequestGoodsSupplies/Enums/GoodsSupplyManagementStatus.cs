
namespace Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

public enum GoodsSupplyManagementStatus
{
    [Description("در انتظار تایید")]
    Pending = 1,
    [Description("ارجاع برای تامین")]
    PendingForConfirme = 11,
    [Description("تامین کامل از ")]
    CompleteSupply = 22,
    [Description("تامین ناقص از ")]
    InCompleteSupply = 33,
    [Description("برگشت از ")]
    Return = 44,
    [Description("برگشت به واحد تامین از ")]
    ReturnToSupply = 55,
    [Description("تایید فاکتور بازرگانی")]
    CommercialInvoiceConfirmation = 153,



    [Description("ثبت اولیه")]
    New = 66,
    [Description("استعلام گیری")]
    Inquiry = 77,
    [Description("تایید استعلام")]
    ConfirmeInquiry = 88,
    [Description("پیش فاکتور")]
    ProformaInvoice = 99,
    [Description("پکینگ")]
    Packing = 100,
    [Description("برگشت به درخواست دهنده")]
    ReturnToRequester = 111,
    [Description("برگشت به واحد تامین")]
    ReturnToTheSupplyUnit = 122,
    [Description("رد درخواست")]
    Reject = 133,
    [Description("ورود از خرید")]
    EntryFromShopping = 144,
    [Description("تحویل موقت")]
    TemporaryDelivery = 155,
    [Description("بین انباری")]
    BetweenWarehouses = 166,
    [Description("خروج")]
    Exit = 177,
    [Description("تامین ناقص")]
    IncompleteSupply = 188,

}
