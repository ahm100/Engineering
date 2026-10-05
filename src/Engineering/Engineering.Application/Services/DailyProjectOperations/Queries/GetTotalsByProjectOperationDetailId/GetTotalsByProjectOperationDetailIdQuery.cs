using DailyProjectOperation = Engineering.Domain.Entities.DailyProjectOperations.DailyProjectOperation;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetTotalsByProjectOperationDetailId;

public record GetTotalsByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId
    ) : IQuery<List<DailyProjectOperation>>;