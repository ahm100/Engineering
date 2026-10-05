using Gita.Backend.Shared.Domain.Enums.PaymentOrder;

namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Models.PaymentOrderMessage;


public class ValidatePaymentOrderStatus
{
    public static List<PaymentOrderStatus> AllowForChangeStatus =
    [
        PaymentOrderStatus.Rejected,
        PaymentOrderStatus.Paid,
        PaymentOrderStatus.IncompletelyPaid,
    ];
}

