
namespace Engineering.Application.Services.ProjectTypes.Models.ProjectTypeGroupDelete;

public record ProjectTypeGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
