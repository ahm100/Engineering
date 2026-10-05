namespace Engineering.Application.Services.TransportationContractors.Contracts.DeleteTransportationContractor;

public record DeleteTransportationContractorRequest(
    List<long> Ids
    ) : IHttpRequest;
