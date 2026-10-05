using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.DeleteTelegramChat;

public class DeleteTelegramChatCommandHandler : ICommandHandler<DeleteTelegramChatCommand, TelegramChat>
{
    private readonly ILogger<DeleteTelegramChatCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public DeleteTelegramChatCommandHandler(ILogger<DeleteTelegramChatCommand> logger, ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(DeleteTelegramChatCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramChat>(TelegramChatErrors.TelegramChatWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<TelegramChat>(TelegramChatErrors.IsDeleted);

            entity.SetIsDeleted();

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