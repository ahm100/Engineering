using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedServiceContract;

public class GetsRequestedServiceContractQueryHandler : IQueryHandler<GetsRequestedServiceContractQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly ILogger<GetsRequestedServiceContractQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetsRequestedServiceContractQueryHandler(ILogger<GetsRequestedServiceContractQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetsRequestedServiceContractQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestedServiceContract(request.ProjectId, request.ServiceIds, request.ContractorId, request.CompanyId, ct);

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
