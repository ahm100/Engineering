using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectServiceDetail;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectServiceDetail;

public class GetsProjectServiceDetailQueryHandler : IQueryHandler<GetsProjectServiceDetailQuery, DataResult<List<GetsProjectServiceDetailModel>>>
{
    private readonly IProjectServiceDetailRepository _repository;
    private readonly ILogger<GetsProjectServiceDetailQueryHandler> _logger;

    public GetsProjectServiceDetailQueryHandler(ILogger<GetsProjectServiceDetailQueryHandler> logger, IProjectServiceDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectServiceDetailModel>>?>> Handle(GetsProjectServiceDetailQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectServiceDetail(
                request.Ids,
                request.CostcenterIds,
                request.ProjectIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.ProjectOperationIds,
                request.IsActive,
                request.ServiceFilterData,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectServiceDetailModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectServiceDetailModel>>>(ProjectServiceErrors.ProjectServicesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectServiceDetailModel>>>(SharedErrors.UnknownError);
        }
    }
}