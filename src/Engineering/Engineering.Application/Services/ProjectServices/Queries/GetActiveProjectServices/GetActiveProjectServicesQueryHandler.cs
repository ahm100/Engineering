using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

namespace Engineering.Application.Services.ProjectServices.Queries.GetActiveProjectServices;

public class GetActiveProjectServicesQueryHandler : IQueryHandler<GetActiveProjectServicesQuery, DataResult<List<GetsActiveProjectServiceModel>>>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<GetActiveProjectServicesQuery> _logger;

    public GetActiveProjectServicesQueryHandler(ILogger<GetActiveProjectServicesQuery> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveProjectServiceModel>>?>> Handle(GetActiveProjectServicesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetActiveProjectServices(
                request.Ids,
                request.CostcenterIds,
                request.ProjectIds,
                request.ServiceInfoIds,
                request.ContractorIds,
                request.ServiceFilterData,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsActiveProjectServiceModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsActiveProjectServiceModel>>>(ProjectServiceErrors.ProjectServicesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsActiveProjectServiceModel>>>(SharedErrors.UnknownError);
        }
    }
}