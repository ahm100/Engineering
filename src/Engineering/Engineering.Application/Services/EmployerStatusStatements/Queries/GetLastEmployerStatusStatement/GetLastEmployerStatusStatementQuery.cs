
using EmployerStatusStatement = Engineering.Domain.Entities.EmployerStatusStatements.EmployerStatusStatement;

namespace Engineering.Application.Services.EmployerStatusStatements.Queries.GetLastEmployerStatusStatement;

public record GetLastEmployerStatusStatementQuery(
    long? EmployerId,
    long CostCenterId,
    long ProjectId,
    string? ContractCode
    ) : IQuery<EmployerStatusStatement>;