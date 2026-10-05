namespace Engineering.Domain.Entities.RequestMachineries.Enums;

public enum RequestMachineryPaymentType
{
    [Description("پرداخت نشده")]
    NotPaid = 1,
    [Description("صدور دستور پرداخت")]
    Paid = 2
}
