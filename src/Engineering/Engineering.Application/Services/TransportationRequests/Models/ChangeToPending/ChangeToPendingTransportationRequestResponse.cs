
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToPending;

public record ChangeToPendingTransportationRequestResponse(
    long Id,
    bool StatusChanged
    );
