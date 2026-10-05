using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByCostCenterId;

public record GetByCostCenterIdQuery(
    long CostCenterId,
    long? ProjectId,
    bool? GetOther,
    int PageIndex,
    int PageSize
) : IQuery<DataResult<List<TelegramChat>>>;