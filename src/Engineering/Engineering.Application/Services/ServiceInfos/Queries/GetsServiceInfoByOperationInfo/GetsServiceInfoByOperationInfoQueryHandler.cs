using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetsServiceInfoByOperationInfo;

public class GetsServiceInfoByOperationInfoQueryHandler : IQueryHandler<GetsServiceInfoByOperationInfoQuery, DataResult<List<ServiceInfo>>>
{
    private readonly IServiceInfoRepository _repository;
    private readonly ILogger<GetsServiceInfoByOperationInfoQueryHandler> _logger;

    public GetsServiceInfoByOperationInfoQueryHandler(ILogger<GetsServiceInfoByOperationInfoQueryHandler> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ServiceInfo>>?>> Handle(GetsServiceInfoByOperationInfoQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsServiceInfoByOperationInfo(request.OperationInfoIds, request.FilterData, request.IsActive, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ServiceInfo>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ServiceInfo>>>(ServiceInfoErrors.FilteredServiceInfoNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ServiceInfo>>>(SharedErrors.UnknownError);
        }
    }
}
