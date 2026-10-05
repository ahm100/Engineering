using Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelEnums;

namespace Engineering.Application.Services.EmployerStatusStatements.Models.GetsEmployerStatusStatementExcelExporter;

public record GetsEmployerStatusStatementExcelExporterRequest(
    List<long>? Ids,
    long CostCenterId,
    long ProjectId,
    long? EmployerId,
    long EmployerContractId,
    string? StatusStatementCode,
    List<long>? ProjectOperationIds,
    DateTime? StartDate,
    DateTime? EndDate,
    Domain.Entities.EmployerStatusStatements.Enums.EmployerStatusStatementStatus? Status,
    List<EmployerStatusStatementExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
