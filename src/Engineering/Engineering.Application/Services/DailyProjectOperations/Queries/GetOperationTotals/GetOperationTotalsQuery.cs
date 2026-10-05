using DailyProjectOperation = Engineering.Domain.Entities.DailyProjectOperations.DailyProjectOperation;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetOperationTotals;

public record GetOperationTotalsQuery(
    long ProjectOperationId,
    long? ProjectOperationDetailId
    ) : IQuery<List<DailyProjectOperation>>;