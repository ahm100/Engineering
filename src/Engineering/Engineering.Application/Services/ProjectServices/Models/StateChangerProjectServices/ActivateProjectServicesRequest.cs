
namespace Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;

public record ActivateProjectServicesRequest(
    List<long> Ids
    ) : IHttpRequest;
