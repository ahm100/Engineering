
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetDeductionAmountByProjectOperationDetailId;

public record GetDeductionAmountByProjectOperationDetailIdRequest(
    long ProjectOperationDetailId
     ) : IHttpRequest;
