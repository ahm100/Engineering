using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetailContractorService = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetailContractorService;

namespace Engineering.Application.Services.ProjectOperationDetailContractorServices.Queries.GetsByFilter;

public class GetContractorServicesQueryHandler : IQueryHandler<GetsContractorServiceByFilterQuery, DataResult<List<ProjectOperationDetailContractorService>>>
{
    private readonly IProjectOperationDetailContractorServiceRepository _repository;
    private readonly ILogger<GetContractorServicesQueryHandler> _logger;

    public GetContractorServicesQueryHandler(ILogger<GetContractorServicesQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetailContractorService>>?>> Handle(GetsContractorServiceByFilterQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByFilter(request.FilterData, request.CostCenterId, request.ProjectId, request.ProjectOperationIds,
                request.ServiceInfoIds, request.OrderBy, request.PageIndex, request.PageSize, ct);

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