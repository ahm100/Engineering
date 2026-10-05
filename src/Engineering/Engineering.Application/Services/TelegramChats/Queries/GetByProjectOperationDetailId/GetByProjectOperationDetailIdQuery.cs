using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByProjectOperationDetailId;

public record GetByProjectOperationDetailIdQuery(
    long ProjectOperationDetailId, bool? GetOther
    ) : IQuery<DataResult<List<TelegramChat>>>;