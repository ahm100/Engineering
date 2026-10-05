using Engineering.Application.Services.ProjectOperations.Models.GetsForEmployerStatusStatement;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetsForEmployerStatusStatement;

public record GetsForEmployerStatusStatementQuery(
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
    ) : IQuery<DataResult<List<GetsForEmployerStatusStatementModel>>>;