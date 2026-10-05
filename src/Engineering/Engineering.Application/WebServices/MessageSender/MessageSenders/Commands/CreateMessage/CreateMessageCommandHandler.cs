using Engineering.Application.Abstractions.Interfaces;
using Engineering.Application.WebServices.MessageSender.MessageSenders.Models.CreateMessage;

namespace Engineering.Application.WebServices.MessageSender.MessageSenders.Commands.CreateMessage;

public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand, CreateMessageResponse?>
{
    private readonly ILogger<CreateMessageCommandHandler> _logger;
    private readonly IMessageSenderService _messageSenderService;

    public CreateMessageCommandHandler(ILogger<CreateMessageCommandHandler> logger, IMessageSenderService messageSenderService)
    {
        _logger = logger;
        _messageSenderService = messageSenderService;
    }

    public async Task<Result<CreateMessageResponse?>> Handle(CreateMessageCommand request, CT ct)
    {
        try
        {
            var result = await _messageSenderService.CreateMessage(new CreateMessageRequest(
                request.receiver,
                request.content,
                (int)request.priority,
                (int)request.type),
                ct);

            if (result is null || result.IsFailure)
                return Result.Failure<CreateMessageResponse>(CommercialErrors.ProviderError(result?.Error));

            return result.Value;
        }
        catch (ApiException ex)
        {
            _logger.LogError(ex, ex.Message);
            var content = JsonConvert.DeserializeObject<MessageErrorModel>(ex.Content!);
            var error = new ErrorModel()
            {
                StatusCode = content is null ? 500 : content.Status,
                Code = content is null ? "Bad" : content.Detail,
                Message = content is null ? "" : content.Ttitle
            };
            var response = new FailureModel(false, true, error);
            return Result.Failure<CreateMessageResponse?>(MessageSenderErrors.ProviderError(response!.Error.Message!));
        }
    }



}
