
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByIds;

public record GetsProjectOperationDetailByIdsRequest(
    List<long>? Ids,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
