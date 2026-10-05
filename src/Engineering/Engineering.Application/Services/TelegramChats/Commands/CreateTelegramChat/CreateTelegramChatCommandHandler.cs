using Engineering.Application.Abstractions.Data.TelegramChats;
using TelegramChat = Engineering.Domain.Entities.TelegramChats.TelegramChat;

namespace Engineering.Application.Services.TelegramChats.Commands.CreateTelegramChat;

public class CreateTelegramChatCommandHandler : ICommandHandler<CreateTelegramChatCommand, TelegramChat?>
{
    private readonly ILogger<CreateTelegramChatCommand> _logger;
    private readonly ITelegramChatRepository _repository;

    public CreateTelegramChatCommandHandler(ILogger<CreateTelegramChatCommand> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<TelegramChat?>> Handle(CreateTelegramChatCommand request,
        CT ct)
    {
        try
        {
            var entity = new TelegramChat(request.CostCenter, request.Project, request.ChatName, request.ChatUrl,
                request.ChatId, request.Description, request.IsActive, request.Types);
            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<TelegramChat?>(SharedErrors.UnknownError);
        }
    }
}