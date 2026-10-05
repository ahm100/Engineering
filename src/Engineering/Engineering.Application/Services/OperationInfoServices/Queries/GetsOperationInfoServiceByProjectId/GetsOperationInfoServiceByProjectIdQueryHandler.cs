using Engineering.Application.Abstractions.Data.OperationInfos;
using Engineering.Application.Services.OperationInfoServices.Models.GetsOperationInfoServiceByProjectId;

namespace Engineering.Application.Services.OperationInfoServices.Queries.GetsOperationInfoServiceByProjectId;

public class GetsOperationInfoServiceByProjectIdQueryHandler : IQueryHandler<GetsOperationInfoServiceByProjectIdQuery, DataResult<List<GetsOperationInfoServiceByProjectIdModel>>>
{
    private readonly IOperationInfoServiceRepository _repository;
    private readonly ILogger<GetsOperationInfoServiceByProjectIdQueryHandler> _logger;

    public GetsOperationInfoServiceByProjectIdQueryHandler(
        ILogger<GetsOperationInfoServiceByProjectIdQueryHandler> logger,
        IOperationInfoServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsOperationInfoServiceByProjectIdModel>>?>> Handle(GetsOperationInfoServiceByProjectIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsOperationInfoServiceByProjectId(
                request.ProjectId,
                request.ExcludedServiceInfoId,
                request.ServiceInfoFilters,
                request.OperationInfoFilters,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsOperationInfoServiceByProjectIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsOperationInfoServiceByProjectIdModel>>>(OperationInfoServiceErrors.FilteredOperationInfoServiceNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsOperationInfoServiceByProjectIdModel>>>(SharedErrors.UnknownError);
        }
    }
}
