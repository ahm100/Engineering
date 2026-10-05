
namespace Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;

public record ActivateDetailContractorServicesRequest(
    List<long> Ids
    ) : IHttpRequest;
