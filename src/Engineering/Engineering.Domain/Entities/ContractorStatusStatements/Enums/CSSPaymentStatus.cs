
namespace Engineering.Domain.Entities.ContractorStatusStatements.Enums;

public enum CSSPaymentStatus
{
    [Description("پرداخت نشده")]
    Unpaid = 1,

    [Description("در انتظار پرداخت")]
    AwaitingPayment = 5,

    [Description("بخشی پرداخت شده")]
    PartlyPaid = 10,

    [Description("پرداخت شده")]
    Paid = 15,

    [Description("رد از خزانه")]
    RejectPayment = 15,
}
