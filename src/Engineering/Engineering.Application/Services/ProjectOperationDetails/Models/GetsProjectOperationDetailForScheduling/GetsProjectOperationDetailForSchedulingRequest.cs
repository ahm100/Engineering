
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailForScheduling;

public record GetsProjectOperationDetailForSchedulingRequest(
    long OperationInfoId,
    long OperationLocationId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
