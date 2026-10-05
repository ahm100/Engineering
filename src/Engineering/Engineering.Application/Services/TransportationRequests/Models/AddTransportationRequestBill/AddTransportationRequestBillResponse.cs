
namespace Engineering.Application.Services.TransportationRequests.Models.AddTransportationRequestBill;

public record AddTransportationRequestBillResponse(
    long? Id,
    long? CargoId,
    bool IsUpdated
    );
