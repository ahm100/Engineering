using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsIncludeLess;

public class GetsOperationInfoByIdsIncludeLessQueryHandler : IQueryHandler<GetsOperationInfoByIdsIncludeLessQuery, DataResult<List<OperationInfo>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetsOperationInfoByIdsIncludeLessQueryHandler> _logger;

    public GetsOperationInfoByIdsIncludeLessQueryHandler(ILogger<GetsOperationInfoByIdsIncludeLessQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfo>>?>> Handle(GetsOperationInfoByIdsIncludeLessQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoByIdsIncludeLess(request.Ids, ct);

            return result.Data.Any() ?
                new DataResult<List<OperationInfo>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<OperationInfo>>>(OperationInfoErrors.FilteredOperationInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<OperationInfo>>>(SharedErrors.UnknownError);
        }
    }
}