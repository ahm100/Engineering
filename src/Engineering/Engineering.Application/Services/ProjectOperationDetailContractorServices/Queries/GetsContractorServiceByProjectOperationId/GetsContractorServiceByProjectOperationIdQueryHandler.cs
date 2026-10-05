using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsContractorServiceByProjectOperationId;

public class GetsContractorServiceByProjectOperationIdQueryHandler : IQueryHandler<GetsContractorServiceByProjectOperationIdQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetsContractorServiceByProjectOperationIdQueryHandler> _logger;

    public GetsContractorServiceByProjectOperationIdQueryHandler(ILogger<GetsContractorServiceByProjectOperationIdQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetsContractorServiceByProjectOperationIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorServiceByProjectOperationId(request.ProjectOperationId, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetailContractorService>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(ContractorServiceErrors.NotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetailContractorService>>>(SharedErrors.UnknownError);
        }
    }
}