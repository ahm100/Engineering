namespace Engineering.Application.Services.ProjectOperationDetailDeductions.Models.UpdateProjectOperationDetailDeduction;

public record UpdateProjectOperationDetailDeductionRequest(
    long Id,
    decimal Length,
    decimal Width,
    decimal Height,
    decimal Weight,
    decimal Number
     ) : IHttpRequest;
