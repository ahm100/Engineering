
namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsFilteredEmployerStatusStatement;

public record GetsFilteredEmployerStatusStatementRequest(
    long EmployerId,
    long CostCenterId,
    long ProjectId,
    long EmployerContractId,
    string? StatusStatementCode,
    List<long>? ProjectOperationIds,
    DateTime? StartDate,
    DateTime? EndDate,
    Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus? Status,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
