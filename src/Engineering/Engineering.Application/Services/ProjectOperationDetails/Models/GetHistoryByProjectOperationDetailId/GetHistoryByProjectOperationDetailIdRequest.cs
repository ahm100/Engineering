
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetHistoryByProjectOperationDetailId;

public record GetHistoryByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
