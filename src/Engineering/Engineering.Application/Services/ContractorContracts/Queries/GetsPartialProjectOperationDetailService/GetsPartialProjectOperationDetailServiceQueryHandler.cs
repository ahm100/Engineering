using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsPartialProjectOperationDetailService;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsPartialProjectOperationDetailService;

public class GetsPartialProjectOperationDetailServiceQueryHandler : IQueryHandler<GetsPartialProjectOperationDetailServiceQuery, DataResult<List<GetsPartialProjectOperationDetailServiceModel>>>
{
    private readonly ILogger<GetsPartialProjectOperationDetailServiceQueryHandler> _logger;
    private readonly IProjectOperationDetailContractorServiceRepository _repository;

    public GetsPartialProjectOperationDetailServiceQueryHandler(ILogger<GetsPartialProjectOperationDetailServiceQueryHandler> logger, IProjectOperationDetailContractorServiceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsPartialProjectOperationDetailServiceModel>>?>> Handle(GetsPartialProjectOperationDetailServiceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsPartialProjectOperationDetailService(
                request.CostCenterId,
                request.ProjectId,
                request.ContractorId,
                request.ServiceInfoId,
                request.ProjectOperationDetailServiceIds,
                request.FilterData,
                request.CompanyId,
                ct);

            var response = new DataResult<List<GetsPartialProjectOperationDetailServiceModel>>()
            {
                Data = result ?? new List<GetsPartialProjectOperationDetailServiceModel>(0)
            };

            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsPartialProjectOperationDetailServiceModel>>>(SharedErrors.UnknownError);
        }
    }
}
