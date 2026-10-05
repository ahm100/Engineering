using Engineering.Application.Abstractions.Data.TelegramChats;
using Engineering.Domain.Entities.TelegramChats;

namespace Engineering.Application.Services.TelegramChats.Queries.GetByProjectOperationDetailId;

public class GetByProjectOperationDetailIdQueryHandler : IQueryHandler<GetByProjectOperationDetailIdQuery, DataResult<List<TelegramChat>>>
{
    private readonly ITelegramChatRepository _repository;
    private readonly ILogger<GetByProjectOperationDetailIdQueryHandler> _logger;

    public GetByProjectOperationDetailIdQueryHandler(ILogger<GetByProjectOperationDetailIdQueryHandler> logger,
        ITelegramChatRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<TelegramChat>>?>> Handle(GetByProjectOperationDetailIdQuery request,
        CT ct)
    {
        try
        {
            var items = await _repository.GetByProjectOperationDetailId(
                request.ProjectOperationDetailId, request.GetOther, ct);

            return items.Data.Any()
                ? new DataResult<List<TelegramChat>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                }
                : Result.Failure<DataResult<List<TelegramChat>>>(TelegramChatErrors.FilteredTelegramChatNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<TelegramChat>>>(SharedErrors.UnknownError);
        }
    }
}