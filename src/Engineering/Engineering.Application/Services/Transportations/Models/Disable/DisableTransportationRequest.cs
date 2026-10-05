namespace Engineering.Application.Services.Transportations.Models.Disable;

public record DisableTransportationRequest(
    long Id
     ) : IHttpRequest;
