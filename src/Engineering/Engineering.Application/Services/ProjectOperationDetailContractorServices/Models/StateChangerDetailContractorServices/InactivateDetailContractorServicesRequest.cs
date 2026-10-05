
namespace Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;

public record InactivateDetailContractorServicesRequest(
    List<long> Ids
    ) : IHttpRequest;
