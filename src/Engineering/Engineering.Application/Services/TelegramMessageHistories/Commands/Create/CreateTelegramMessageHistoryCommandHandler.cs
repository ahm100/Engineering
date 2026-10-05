using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramMessageHistorys.Commands.Create;

public class
    CreateTelegramMessageHistoryCommandHandler : ICommandHandler<CreateTelegramMessageHistoryCommand,
    TelegramMessageHistory?>
{
    private readonly ILogger<CreateTelegramMessageHistoryCommand> _logger;
    private readonly ITelegramMessageHistoryRepository _historyRepository;

    public CreateTelegramMessageHistoryCommandHandler(ILogger<CreateTelegramMessageHistoryCommand> logger,
        ITelegramMessageHistoryRepository historyRepository)
    {
        _logger = logger;
        _historyRepository = historyRepository;
    }

    public async Task<Result<TelegramMessageHistory?>> Handle(CreateTelegramMessageHistoryCommand request,
        CT ct)
    {
        try
        {
            var entity = new TelegramMessageHistory(
                request.TelegramChatId,
                request.Message,
                request.ErrorMessage,
                request.FileUrls,
                request.TelegramMessageType,
                request.ChatId,
                request.IsSend);
            var result = await _historyRepository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramMessageHistory?>(SharedErrors.UnknownError);
        }
    }
}