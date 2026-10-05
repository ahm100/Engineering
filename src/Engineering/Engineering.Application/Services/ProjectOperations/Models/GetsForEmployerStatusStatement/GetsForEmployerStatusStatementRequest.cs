namespace Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;

public record GetsForEmployerStatusStatementRequest(
    long? EmployerId,
    long? EmployerStatusStatementId,
    long ProjectId,
    long CostCenterId,
    long? EmployerContractId,
    DateTime? StartDate,
    DateTime? EndDate,
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
