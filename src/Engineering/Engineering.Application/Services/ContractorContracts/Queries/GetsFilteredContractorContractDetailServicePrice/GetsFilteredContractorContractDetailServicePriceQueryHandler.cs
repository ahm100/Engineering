using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailServicePrice;

public class GetsFilteredContractorContractDetailServicePriceQueryHandler : IQueryHandler<GetsFilteredContractorContractDetailServicePriceQuery, DataResult<List<ContractorContractDetailPrice>>>
{
    private readonly ILogger<GetsFilteredContractorContractDetailServicePriceQueryHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public GetsFilteredContractorContractDetailServicePriceQueryHandler(ILogger<GetsFilteredContractorContractDetailServicePriceQueryHandler> logger,
                                                                 IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetailPrice>>?>> Handle(GetsFilteredContractorContractDetailServicePriceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredContractorContractDetailServicePrice(request.ProjectOperationServiceId, request.ServiceInfoId, request.StratDate, request.EndDate,
                request.ContractorId, request.CostCenterId, request.ProjectId, request.FilterData, request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractDetailPrice>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractDetailPrice>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractDetailPrice>>>(SharedErrors.UnknownError);
        }
    }
}
