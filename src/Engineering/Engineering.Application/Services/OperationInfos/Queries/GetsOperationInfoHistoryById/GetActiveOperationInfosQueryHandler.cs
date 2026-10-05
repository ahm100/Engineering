using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfos.Models.GetsOperationInfoHistoryById;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoHistoryById;

public class GetsOperationInfoHistoryByIdQueryHandler : IQueryHandler<GetsOperationInfoHistoryByIdQuery, DataResult<List<GetsOperationInfoHistoryByIdModel>>>
{
    private readonly IOperationInfoHistoryRepository _repository;
    private readonly ILogger<GetsOperationInfoHistoryByIdQuery> _logger;

    public GetsOperationInfoHistoryByIdQueryHandler(ILogger<GetsOperationInfoHistoryByIdQuery> logger, IOperationInfoHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsOperationInfoHistoryByIdModel>>?>> Handle(GetsOperationInfoHistoryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoHistoryById(
                request.OperationInfoId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsOperationInfoHistoryByIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsOperationInfoHistoryByIdModel>>>(OperationInfoErrors.FilteredOperationInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsOperationInfoHistoryByIdModel>>>(SharedErrors.UnknownError);
        }
    }
}