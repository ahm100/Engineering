using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsIntegratedProjectOperationDetailService;

public class GetsIntegratedProjectOperationDetailServiceQueryHandler : IQueryHandler<GetsIntegratedProjectOperationDetailServiceQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly ILogger<GetsIntegratedProjectOperationDetailServiceQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetsIntegratedProjectOperationDetailServiceQueryHandler(
        ILogger<GetsIntegratedProjectOperationDetailServiceQueryHandler> logger,
        IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetsIntegratedProjectOperationDetailServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsIntegratedProjectOperationDetailService(
                request.ProjectOperationDetailServiceIds,
                request.CostCenterId,
                request.ProjectId,
                request.ContractorId,
                request.ProjectOperationIds,
                request.ServiceInfoIds,
                request.FilterData,
                request.CompanyId,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailContractorService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(SharedErrors.UnknownError);
        }
    }
}
