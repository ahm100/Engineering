using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.ActiveTelegramChat;

public class ActiveTelegramChatCommandHandler : ICommandHandler<ActiveTelegramChatCommand, TelegramChat>
{
    private readonly ILogger<ActiveTelegramChatCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public ActiveTelegramChatCommandHandler(ILogger<ActiveTelegramChatCommand> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(ActiveTelegramChatCommand request,
        CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramChat>(TelegramChatErrors.TelegramChatWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<TelegramChat>(TelegramChatErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<TelegramChat>(TelegramChatErrors.IsDeleted);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramChat>(SharedErrors.UnknownError);
        }
    }
}