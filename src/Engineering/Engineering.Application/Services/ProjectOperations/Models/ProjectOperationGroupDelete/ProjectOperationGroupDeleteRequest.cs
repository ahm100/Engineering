
namespace Engineering.Application.Services.ProjectOperations.Models.ProjectOperationGroupDelete;

public record ProjectOperationGroupDeleteRequest(
    List<long> Ids
    ) : IHttpRequest;
