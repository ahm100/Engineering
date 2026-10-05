using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.InactiveTelegramChat;

public class InactiveTelegramChatCommandHandler : ICommandHandler<InactiveTelegramChatCommand, TelegramChat>
{
    private readonly ILogger<InactiveTelegramChatCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public InactiveTelegramChatCommandHandler(ILogger<InactiveTelegramChatCommand> logger, ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(InactiveTelegramChatCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramChat>(TelegramChatErrors.TelegramChatWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<TelegramChat>(TelegramChatErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<TelegramChat>(TelegramChatErrors.IsDeleted);

            entity.SetInActive();
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