
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsMinimalByProjectOperationIds;

public record GetsMinimalByProjectOperationIdsRequest(
    List<long>? ProjectOperationIds,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
