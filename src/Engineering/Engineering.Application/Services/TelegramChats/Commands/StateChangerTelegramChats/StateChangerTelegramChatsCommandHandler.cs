using Engineering.Application.Abstractions.Data.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Commands.StateChangerTelegramChats;

public class StateChangerTelegramChatsCommandHandler : ICommandHandler<StateChangerTelegramChatsCommand, bool?>
{
    private readonly ILogger<StateChangerTelegramChatsCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public StateChangerTelegramChatsCommandHandler(ILogger<StateChangerTelegramChatsCommand> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerTelegramChatsCommand request,
        CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}