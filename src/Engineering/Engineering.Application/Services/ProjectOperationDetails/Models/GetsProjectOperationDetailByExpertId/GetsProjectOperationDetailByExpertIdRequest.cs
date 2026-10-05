
namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetsProjectOperationDetailByExpertId;

public record GetsProjectOperationDetailByExpertIdRequest(
    long ProjectOperationId,
    long ExpertId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
