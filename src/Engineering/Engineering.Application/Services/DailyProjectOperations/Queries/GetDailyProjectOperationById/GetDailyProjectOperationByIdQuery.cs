using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationById;

public record GetDailyProjectOperationByIdQuery(
    long DailyProjectOperationId
    ) : IQuery<DailyProjectOperation>;
