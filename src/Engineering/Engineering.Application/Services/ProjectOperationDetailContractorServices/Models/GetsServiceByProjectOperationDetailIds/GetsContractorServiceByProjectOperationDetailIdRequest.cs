
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsServiceByProjectOperationDetailIds;

public record GetsServiceByProjectOperationDetailIdsRequest(
    List<long> ProjectOperationDetailIds,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
