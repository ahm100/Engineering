using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetEmployerStatusStatementLimitDate;

public record GetEmployerStatusStatementLimitDateQuery(
    long ProjectId,
    long EmployerContractId
    ) : IQuery<List<DailyProjectOperation>>;
