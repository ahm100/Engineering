using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationsByProjectOperationIds;

public record GetDailyProjectOperationsByProjectOperationIdsQuery(
    List<long> ProjectOperationIds
    ) : IQuery<List<DailyProjectOperation>>;
