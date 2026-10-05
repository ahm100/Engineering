
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByProductId;

public record GetsProjectOperationDetailByProductIdRequest(
    long ProjectOperationId,
    long ProductId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
