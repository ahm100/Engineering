
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.GetProjectOperationDetailDeductionById;

public record GetProjectOperationDetailDeductionByIdRequest(
    long Id
     ) : IHttpRequest;