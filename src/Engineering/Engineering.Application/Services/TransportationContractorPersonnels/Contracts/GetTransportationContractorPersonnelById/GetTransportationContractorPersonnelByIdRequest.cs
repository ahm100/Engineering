
namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetTransportationContractorPersonnelById;

public record GetTransportationContractorPersonnelByIdRequest(
    long Id
    ) : IHttpRequest;
