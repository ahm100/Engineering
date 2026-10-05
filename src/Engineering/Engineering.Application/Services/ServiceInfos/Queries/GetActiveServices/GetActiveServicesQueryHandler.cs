using Engineering.Application.Abstractions.Data.ServiceInfos;
using ServiceInfo = Engineering.Domain.Entities.ServiceInfos.ServiceInfo;

namespace Engineering.Application.Services.ServiceInfos.Queries.GetActiveServices;

public class GetActiveServiceInfosQueryHandler : IQueryHandler<GetActiveServiceInfosQuery, DataResult<List<ServiceInfo>>>
{
    private readonly IServiceInfoRepository _repository;
    private readonly ILogger<GetActiveServiceInfosQuery> _logger;

    public GetActiveServiceInfosQueryHandler(ILogger<GetActiveServiceInfosQuery> logger, IServiceInfoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ServiceInfo>>?>> Handle(GetActiveServiceInfosQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveServiceInfos(
                request.FilterData,
                request.ServiceInfoCode,
                request.ServiceInfoName,
                request.ProjectId,
                request.CompanyId,
                request.PageIndex,
                request.PageSize,
                ct);

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