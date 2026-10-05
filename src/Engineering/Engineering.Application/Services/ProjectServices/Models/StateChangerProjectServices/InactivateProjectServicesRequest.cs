
namespace Engineering.Application.Services.ProjectServices.Models.StateChangerProjectServices;

public record InactivateProjectServicesRequest(
    List<long> Ids
    ) : IHttpRequest;
