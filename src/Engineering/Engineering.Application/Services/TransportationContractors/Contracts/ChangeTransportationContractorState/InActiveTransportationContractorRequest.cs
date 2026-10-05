namespace Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;

public record InActiveTransportationContractorRequest(
    long Id
    ) : IHttpRequest;
