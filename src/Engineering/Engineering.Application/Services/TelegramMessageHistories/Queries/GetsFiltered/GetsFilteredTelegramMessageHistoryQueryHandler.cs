using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;


namespace Engineering.Application.Services.TelegramMessageHistorys.Queries.GetsFiltered;

public class GetsFilteredTelegramMessageHistoryQueryHandler : IQueryHandler<GetsFilteredTelegramMessageHistoryQuery,
    DataResult<List<TelegramMessageHistory>>>
{
    private readonly ITelegramMessageHistoryRepository _historyRepository;
    private readonly ILogger<GetsFilteredTelegramMessageHistoryQueryHandler> _logger;

    public GetsFilteredTelegramMessageHistoryQueryHandler(ILogger<GetsFilteredTelegramMessageHistoryQueryHandler> logger,
        ITelegramMessageHistoryRepository historyRepository)
    {
        _logger = logger;
        _historyRepository = historyRepository;
    }

    public async Task<Result<DataResult<List<TelegramMessageHistory>>?>> Handle(
        GetsFilteredTelegramMessageHistoryQuery request, CT ct)
    {
        try
        {
            var items = await _historyRepository.GetsFilteredTelegramMessageHistory(
                request.Ids,
                request.FilterData,
                request.MessageType,
                request.IsSend,
                request.PageIndex,
                request.PageSize,
                ct);

            return items.Data.Any()
                ? new DataResult<List<TelegramMessageHistory>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                }
                : Result.Failure<DataResult<List<TelegramMessageHistory>>>(TelegramMessageHistoryErrors
                    .FilteredTelegramMessageHistoryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<TelegramMessageHistory>>>(SharedErrors.UnknownError);
        }
    }
}