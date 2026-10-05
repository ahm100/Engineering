
namespace Engineering.Application.WebServices.Treasury.PaymentOrders.Commands.DeletePaymentOrder;

public record DeletePaymentOrderCommand(
    long Id
    ) : ICommand<bool>;


public record DeletePaymentOrderRequest(
    long Id
    );

public record DeletePaymentOrderResponse(
    long Id
    );
