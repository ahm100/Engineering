using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByCostCenterIds;

public class
    GetsTelegramChatByCostCenterIdsQueryHandler : IQueryHandler<GetsTelegramChatByCostCenterIdsQuery,
    DataResult<List<TelegramChat>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetsTelegramChatByCostCenterIdsQuery> _logger;

    public GetsTelegramChatByCostCenterIdsQueryHandler(ILogger<GetsTelegramChatByCostCenterIdsQuery> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramChat>>?>> Handle(GetsTelegramChatByCostCenterIdsQuery request,
        CT ct)
    {
        try
        {
            var items = await _repository.GetsTelegramChatByCostCenterIds(
                request.CostCenterIds,
                request.TelegramMessageType,
                request.FilterData,
                request.IsActive,
                request.GetOther,
                request.PageIndex,
                request.PageSize, ct);

            return items.Data.Any()
                ? new DataResult<List<TelegramChat>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                }
                : Result.Failure<DataResult<List<TelegramChat>>>(TelegramChatErrors.FilteredTelegramChatNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<TelegramChat>>>(SharedErrors.UnknownError);
        }
    }
}