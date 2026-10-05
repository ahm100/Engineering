namespace Engineering.Application.Services.TransportationRequests.Models.AddTransportationRequestBill;

public record AddTransportationRequestBillRequest(
    long? Id,
    long? CargoId,
    List<string> Documents
) : IHttpRequest;
