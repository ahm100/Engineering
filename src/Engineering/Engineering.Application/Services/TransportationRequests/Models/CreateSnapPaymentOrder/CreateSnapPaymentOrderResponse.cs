namespace Engineering.Application.Services.TransportationRequests.Models.CreateSnapPaymentOrder;

public record CreateSnapPaymentOrderResponse(
    List<long> Ids,
    long PaymentOrderId,
    bool Created
    );
