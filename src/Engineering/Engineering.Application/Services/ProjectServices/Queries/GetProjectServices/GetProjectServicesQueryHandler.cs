using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Services.ProjectServices.Models.ProjectServiceModels;

namespace Engineering.Application.Services.ProjectServices.Queries.GetProjectServices;

public class GetProjectServicesQueryHandler : IQueryHandler<GetProjectServicesQuery, DataResult<List<GetsContractorServiceModel>>>
{
    private readonly IProjectServiceRepository _repository;
    private readonly ILogger<GetProjectServicesQueryHandler> _logger;

    public GetProjectServicesQueryHandler(ILogger<GetProjectServicesQueryHandler> logger, IProjectServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsContractorServiceModel>>?>> Handle(GetProjectServicesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectServices(
                request.ProjectId,
                request.FilterData,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsContractorServiceModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsContractorServiceModel>>>(ProjectServiceErrors.ProjectServiceWithProjectIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorServiceModel>>>(SharedErrors.UnknownError);
        }
    }
}