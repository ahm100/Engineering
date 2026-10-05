using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatByName;

public class GetTelegramChatByNameQueryHandler : IQueryHandler<GetTelegramChatByNameQuery, TelegramChat?>
{
    private readonly ILogger<GetTelegramChatByNameQueryHandler> _logger;
    private readonly ITelegramChatRepository _repository;

    public GetTelegramChatByNameQueryHandler(ILogger<GetTelegramChatByNameQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(GetTelegramChatByNameQuery request,
        CT ct)
    {
        try
        {
            var item = await _repository.FindByName(request.ChatName, ct);

            return item ?? Result.Failure<TelegramChat?>(TelegramChatErrors.TelegramChatWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramChat?>(SharedErrors.UnknownError);
        }
    }
}