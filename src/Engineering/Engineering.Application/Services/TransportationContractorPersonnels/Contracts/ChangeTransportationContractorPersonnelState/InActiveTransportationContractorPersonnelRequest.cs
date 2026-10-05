namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;

public record InActiveTransportationContractorPersonnelRequest(
    long Id
    ) : IHttpRequest;
