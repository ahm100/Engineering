using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChats;

public class GetTelegramChatsQueryHandler : IQueryHandler<GetTelegramChatsQuery, DataResult<List<TelegramChat>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetTelegramChatsQueryHandler> _logger;

    public GetTelegramChatsQueryHandler(ILogger<GetTelegramChatsQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramChat>>?>> Handle(GetTelegramChatsQuery request,
        CT ct)
    {
        try
        {
            var items = await _repository.GetTelegramChats(
                request.Ids,
                request.TelegramMessageType,
                request.FilterData,
                request.CostCenterId,
                request.ProjectId,
                request.Name,
                request.IsActive,
                request.OrderBy,
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