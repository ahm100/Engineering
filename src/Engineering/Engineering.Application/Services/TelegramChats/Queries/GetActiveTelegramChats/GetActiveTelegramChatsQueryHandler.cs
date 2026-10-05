using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetActiveTelegramChats;

public class
    GetActiveTelegramChatsQueryHandler : IQueryHandler<GetActiveTelegramChatsQuery, DataResult<List<TelegramChat>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetActiveTelegramChatsQuery> _logger;

    public GetActiveTelegramChatsQueryHandler(ILogger<GetActiveTelegramChatsQuery> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramChat>>?>> Handle(GetActiveTelegramChatsQuery request,
        CT ct)
    {
        try
        {
            var result = await _repository.GetActiveTelegramChats(
                request.FilterData,
                request.CostCenterId,
                request.ProjectId,
                request.Name,
                request.GetOther,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any()
                ? new DataResult<List<TelegramChat>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                }
                : Result.Failure<DataResult<List<TelegramChat>>>(TelegramChatErrors.TelegramChatWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<TelegramChat>>>(SharedErrors.UnknownError);
        }
    }
}