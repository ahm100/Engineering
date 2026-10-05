
namespace Engineering.Application.Services.Projects.Models.ProjectGroupDelete;

public record ProjectGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
