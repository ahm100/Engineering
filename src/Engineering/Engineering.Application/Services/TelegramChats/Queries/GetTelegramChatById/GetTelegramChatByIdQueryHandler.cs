using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Queries.GetTelegramChatById;

public class GetTelegramChatByIdQueryHandler : IQueryHandler<GetTelegramChatByIdQuery, TelegramChat?>
{
    private readonly ILogger<GetTelegramChatByIdQueryHandler> _logger;
    private readonly ITelegramChatRepository _repository;

    public GetTelegramChatByIdQueryHandler(ILogger<GetTelegramChatByIdQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(GetTelegramChatByIdQuery request,
        CT ct)
    {
        try
        {
            var item = await _repository.GetById(request.Id, ct);

            return item ?? Result.Failure<TelegramChat?>(TelegramChatErrors.TelegramChatWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramChat?>(SharedErrors.UnknownError);
        }
    }
}