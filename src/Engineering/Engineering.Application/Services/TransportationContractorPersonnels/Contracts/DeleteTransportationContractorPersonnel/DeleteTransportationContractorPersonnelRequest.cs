namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.DeleteTransportationContractorPersonnel;

public record DeleteTransportationContractorPersonnelRequest(
    List<long> Ids
    ) : IHttpRequest;
