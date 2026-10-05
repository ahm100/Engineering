namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.ChangeTransportationContractorPersonnelState;

public record ActiveTransportationContractorPersonnelRequest(
    long Id
    ) : IHttpRequest;
