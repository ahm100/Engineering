using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationByLegacyId;

public record GetDailyProjectOperationByLegacyIdQuery(
    long LegacyId) : IQuery<DailyProjectOperation>;
