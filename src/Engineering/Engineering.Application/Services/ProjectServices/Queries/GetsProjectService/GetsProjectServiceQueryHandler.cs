using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.GetsProjectService;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsProjectService;

public class GetsProjectServiceQueryHandler : IQueryHandler<GetsProjectServiceQuery, DataResult<List<GetsProjectServiceModel>>>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<GetsProjectServiceQueryHandler> _logger;

    public GetsProjectServiceQueryHandler(ILogger<GetsProjectServiceQueryHandler> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsProjectServiceModel>>?>> Handle(GetsProjectServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsProjectService(
                request.Ids,
                request.CostcenterIds,
                request.ProjectIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.IsActive,
                request.ServiceFilterData,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsProjectServiceModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsProjectServiceModel>>>(ProjectServiceErrors.ProjectServicesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsProjectServiceModel>>>(SharedErrors.UnknownError);
        }
    }
}