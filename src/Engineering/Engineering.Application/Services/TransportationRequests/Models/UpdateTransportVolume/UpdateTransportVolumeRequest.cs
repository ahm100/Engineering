namespace Engineering.Application.Services.TransportationRequests.Models.UpdateTransportVolume;

public record UpdateTransportVolumeRequest(
    long Id,
    decimal Volume
     ) : IHttpRequest;
