namespace Engineering.Application.Services.TransportationRequests.Models.CreateTransportationRequestPaymentOrder;

public record CreateTransportationRequestPaymentOrderResponse(
    long Id,
    long PaymentOrderId,
    bool Created
    );
