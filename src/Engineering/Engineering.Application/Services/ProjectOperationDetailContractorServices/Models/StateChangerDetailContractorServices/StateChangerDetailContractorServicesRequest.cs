
namespace Engineering.Application.Services.DetailContractorServices.Models.StateChangerDetailContractorServices;

public record StateChangerDetailContractorServicesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
