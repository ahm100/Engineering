
namespace Engineering.Application.Services.Projects.Models.StateChangerProjects;

public record InactivateProjectsRequest(
    List<long> Ids
    ) : IHttpRequest;
