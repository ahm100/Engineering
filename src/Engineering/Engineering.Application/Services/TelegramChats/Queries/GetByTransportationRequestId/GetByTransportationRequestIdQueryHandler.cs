using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Application.Services.TelegramChats.Models.ResponseModels;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByTransportationRequestId;

public class GetByTransportationRequestIdQueryHandler : IQueryHandler<GetByTransportationRequestIdQuery, DataResult<List<TelegramMessageResponseModel>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetByTransportationRequestIdQueryHandler> _logger;

    public GetByTransportationRequestIdQueryHandler(ILogger<GetByTransportationRequestIdQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramMessageResponseModel>>?>> Handle(GetByTransportationRequestIdQuery request,
        CT ct)
    {
        try
        {
            var items = await _repository.GetByTransportationRequestId(
                request.TransportationRequestId, request.GetOther, request.telegramMessageType, ct);

            return items.Data.Any()
                ? new DataResult<List<TelegramMessageResponseModel>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                }
                : Result.Failure<DataResult<List<TelegramMessageResponseModel>>>(TelegramChatErrors.FilteredTelegramChatNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<TelegramMessageResponseModel>>>(SharedErrors.UnknownError);
        }
    }
}