
namespace Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;

public record StateChangerProjectServicesRequest(
    List<long> Ids,
    bool State
    ) : IHttpRequest;
