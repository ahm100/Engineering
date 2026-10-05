namespace Engineering.Application.Services.TransportationRequests.Models.CreateAirPlanePaymentOrder;

public record CreateAirPlanePaymentOrderResponse(
    long Id,
    long PaymentOrderId,
    bool Created
    );
