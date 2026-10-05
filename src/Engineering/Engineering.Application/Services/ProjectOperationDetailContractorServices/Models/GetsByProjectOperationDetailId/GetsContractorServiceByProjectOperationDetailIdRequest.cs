
namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Models.GetsByProjectOperationDetailId;

public record GetsContractorServiceByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    string? ServiceInfoName,
    string? ServiceInfoCode,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
