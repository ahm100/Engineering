
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetsDeductionByProjectOperationDetailId;

public record GetsDeductionByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
