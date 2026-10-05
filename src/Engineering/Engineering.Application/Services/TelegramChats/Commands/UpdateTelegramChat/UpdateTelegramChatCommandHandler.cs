using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.UpdateTelegramChat;

public class UpdateTelegramChatCommandHandler : ICommandHandler<UpdateTelegramChatCommand, TelegramChat>
{
    private readonly ILogger<UpdateTelegramChatCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public UpdateTelegramChatCommandHandler(ILogger<UpdateTelegramChatCommand> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(UpdateTelegramChatCommand request,
        CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<TelegramChat>(TelegramChatErrors.TelegramChatWithIdNotFound);

            entity.SetCostCenter(request.CostCenter);
            entity.SetProject(request.Project);
            entity.SetChatName(request.ChatName);
            entity.SetDescription(request.Description);
            entity.SetChatUrl(request.ChatUrl);
            entity.SetChatId(request.ChatId);
            entity.AddTelegramChatTypes(request.Types, request.ChatId, request.ChatUrl, request.ChatName);

            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<TelegramChat>(SharedErrors.UnknownError);
        }
    }
}