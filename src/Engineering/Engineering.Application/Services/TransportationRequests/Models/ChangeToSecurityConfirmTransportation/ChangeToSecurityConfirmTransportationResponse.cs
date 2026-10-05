
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToSecurityConfirmTransportation;

public record ChangeToSecurityConfirmTransportationResponse(
    long? Id,
    long? CargoId,
    bool StatusChanged
    );
