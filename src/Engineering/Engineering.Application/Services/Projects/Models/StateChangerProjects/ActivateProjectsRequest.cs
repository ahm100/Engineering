
namespace Engineering.Application.Services.Projects.Models.StateChangerProjects;

public record ActivateProjectsRequest(
    List<long> Ids
    ) : IHttpRequest;
