using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationHistoryById;

public record GetDailyProjectOperationHistoryByIdQuery(
    long Id
    ) : IQuery<GetDailyProjectOperationHistoryByIdResponse?>;
