
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsSummarizedByProjectOperationIds;

public record GetsSummarizedByProjectOperationIdsRequest(
    List<long> ProjectOperationIds,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
