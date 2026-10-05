
namespace Engineering.Application.Services.TransportationRequests.Models.ChangeToSecurityConfirmTransportation;

public record ChangeToSecurityConfirmTransportationRequest(
    long? Id,
    long? CargoId,
    string? ManagerDescription
     ) : IHttpRequest;
