
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByMachineryId;

public record GetsProjectOperationDetailByMachineryIdRequest(
    long ProjectOperationId,
    long MachineryId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
