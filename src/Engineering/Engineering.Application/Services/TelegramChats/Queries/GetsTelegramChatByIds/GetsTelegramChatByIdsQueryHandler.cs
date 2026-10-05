using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetsTelegramChatByIds;

public class GetsTelegramChatByIdsQueryHandler : IQueryHandler<GetsTelegramChatByIdsQuery, List<TelegramChat>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetsTelegramChatByIdsQuery> _logger;

    public GetsTelegramChatByIdsQueryHandler(ILogger<GetsTelegramChatByIdsQuery> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<TelegramChat>?>> Handle(GetsTelegramChatByIdsQuery request,
        CT ct)
    {
        try
        {
            var result = await _repository.GetsTelegramChatByIds(request.Ids, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<TelegramChat>>(SharedErrors.UnknownError);
        }
    }
}