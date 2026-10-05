using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsRequestedOperationContract;

public class GetsRequestedOperationContractQueryHandler : IQueryHandler<GetsRequestedOperationContractQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly ILogger<GetsRequestedOperationContractQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetsRequestedOperationContractQueryHandler(ILogger<GetsRequestedOperationContractQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetsRequestedOperationContractQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsRequestedOperationContract(request.ProjectOperationDetailServiceIds, request.CompanyId, ct);

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
