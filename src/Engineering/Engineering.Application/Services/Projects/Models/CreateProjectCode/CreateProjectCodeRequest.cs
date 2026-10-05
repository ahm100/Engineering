namespace Engineering.Application.Services.Projects.Models.CreateProjectCode;

public record CreateProjectCodeRequest(
    long EmployerId,
    long? CostCenterId
     ) : IHttpRequest;
