namespace Engineering.Application.Services.Transportations.Models.GetByCode;

public record GetTransportationByCodeRequest(
    string TransportationCode
     ) : IHttpRequest;
