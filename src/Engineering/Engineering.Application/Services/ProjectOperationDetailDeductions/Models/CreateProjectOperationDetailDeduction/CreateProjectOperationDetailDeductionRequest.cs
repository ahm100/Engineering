
namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.CreateProjectOperationDetailDeduction;

public record CreateProjectOperationDetailDeductionRequest(
    long ProjectOperationDetailId,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number
     ) : IHttpRequest;
