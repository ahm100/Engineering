namespace Engineering.Application.Services.TransportationRequests.Models.PackingRivision;

public record PackingRivisionRequest(
    List<long>? CargoIds,
    List<long>? PackingIds,
    string? Description
     ) : IHttpRequest;
