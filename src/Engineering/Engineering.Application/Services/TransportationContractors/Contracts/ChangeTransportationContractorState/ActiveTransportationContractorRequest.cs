namespace Engineering.Application.Services.TransportationContractors.Contracts.ChangeTransportationContractorState;

public record ActiveTransportationContractorRequest(
    long Id
    ) : IHttpRequest;
