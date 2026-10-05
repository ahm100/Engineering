using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Domain.Entities.OperationInfos;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByIds;

public class GetsOperationInfoServiceByIdsQueryHandler : IQueryHandler<GetsOperationInfoServiceByIdsQuery, DataResult<List<OperationInfoService>>>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetsOperationInfoServiceByIdsQueryHandler> _logger;

    public GetsOperationInfoServiceByIdsQueryHandler(
        ILogger<GetsOperationInfoServiceByIdsQueryHandler> logger,
        IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfoService>>?>> Handle(GetsOperationInfoServiceByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoServiceByIds(
                request.Ids,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfoService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfoService>>>(OperationInfoServiceErrors.FilteredOperationInfoServiceNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfoService>>>(SharedErrors.UnknownError);
        }
    }
}
