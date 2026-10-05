
namespace Engineering.Application.Services.TransportationContractors.Contracts.GetTransportationContractorById;

public record GetTransportationContractorByIdRequest(
    long Id
    ) : IHttpRequest;
