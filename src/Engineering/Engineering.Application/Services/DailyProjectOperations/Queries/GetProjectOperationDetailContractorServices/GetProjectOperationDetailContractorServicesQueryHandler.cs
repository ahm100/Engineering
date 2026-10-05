using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Queries.GetProjectOperationDetailContractorServices;

public class GetProjectOperationDetailContractorServicesQueryHandler : IQueryHandler<GetProjectOperationDetailContractorServicesQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly ILogger<GetProjectOperationDetailContractorServicesQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetProjectOperationDetailContractorServicesQueryHandler(
        ILogger<GetProjectOperationDetailContractorServicesQueryHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetProjectOperationDetailContractorServicesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorServiceForDaily(
                request.ProjectOperationDetailId,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailContractorService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(CostCenterErrors.FilteredCostCenterNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(SharedErrors.UnknownError);
        }
    }
}
