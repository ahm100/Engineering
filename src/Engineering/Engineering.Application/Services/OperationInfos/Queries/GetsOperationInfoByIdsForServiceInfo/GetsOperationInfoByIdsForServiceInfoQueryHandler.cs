using Engineering.Application.Abstractions.Data.OperationInfos;
using OperationInfo = Engineering.Domain.Entities.OperationInfos.OperationInfo;

namespace Engineering.Application.Services.OperationInfos.Queries.GetsOperationInfoByIdsForServiceInfo;

public class GetsOperationInfoByIdsForServiceInfoQueryHandler : IQueryHandler<GetsOperationInfoByIdsForServiceInfoQuery, DataResult<List<OperationInfo>>>
{
    private readonly IOperationInfoRepository _repository;
    private readonly ILogger<GetsOperationInfoByIdsForServiceInfoQueryHandler> _logger;

    public GetsOperationInfoByIdsForServiceInfoQueryHandler(ILogger<GetsOperationInfoByIdsForServiceInfoQueryHandler> logger, IOperationInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<OperationInfo>>?>> Handle(GetsOperationInfoByIdsForServiceInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoByIdsForServiceInfo(request.Ids, request.PageIndex, request.PageSize, ct);

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