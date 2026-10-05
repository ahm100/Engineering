using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetDailyProjectOperationHistoryById;

public class GetDailyProjectOperationHistoryByIdQueryHandler : IQueryHandler<GetDailyProjectOperationHistoryByIdQuery, GetDailyProjectOperationHistoryByIdResponse?>
{
    private readonly ILogger<GetDailyProjectOperationHistoryByIdQueryHandler> _logger;
    private readonly IDailyProjectOperationHistoryRepository _repository;

    public GetDailyProjectOperationHistoryByIdQueryHandler(ILogger<GetDailyProjectOperationHistoryByIdQueryHandler> logger,
                                                    IDailyProjectOperationHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetDailyProjectOperationHistoryByIdResponse?>> Handle(GetDailyProjectOperationHistoryByIdQuery request, CT ct)
    {
        try
        {
            var response = await _repository.GetDailyProjectOperationHistoryById(request.Id, ct);
            if (response is null)
                return Result.Failure<GetDailyProjectOperationHistoryByIdResponse>(DailyProjectOperationErrors.HistoryWithIdNotFound)!;

            var result = new GetDailyProjectOperationHistoryByIdResponse(response, response.Count);
            return result ?? Result.Failure<GetDailyProjectOperationHistoryByIdResponse?>(DailyProjectOperationErrors.DailyProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetDailyProjectOperationHistoryByIdResponse?>(SharedErrors.UnknownError);
        }
    }
}
