using Engineering.Application.Abstractions.Data.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectServices.Queries.GetsContractorProjectService;

public class GetsContractorProjectServiceQueryHandler : IQueryHandler<GetsContractorProjectServiceQuery, DataResult<List<long>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsContractorProjectServiceQuery> _logger;

    public GetsContractorProjectServiceQueryHandler(
        ILogger<GetsContractorProjectServiceQuery> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<long>>?>> Handle(GetsContractorProjectServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorProjectService(
                request.ProjectId,
                request.ServiceInfoId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<long>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<long>>>(ProjectServiceErrors.ProjectServicesNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<long>>>(SharedErrors.UnknownError);
        }
    }
}