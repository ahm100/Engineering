using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetBySnapRequestIds;

public class GetBySnapRequestIdsQueryHandler : IQueryHandler<GetBySnapRequestIdsQuery, DataResult<List<TelegramChat>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetBySnapRequestIdsQueryHandler> _logger;

    public GetBySnapRequestIdsQueryHandler(ILogger<GetBySnapRequestIdsQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramChat>>?>> Handle(GetBySnapRequestIdsQuery request,
        CT ct)
    {
        try
        {
            var items = await _repository.GetBySnapIds(
                request.SnapRequestIds,
                request.GetOther,
                request.telegramMessageType,
                ct);

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