using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsFilteredContractorContractDetailOperationPrice;

public class GetsFilteredContractorContractDetailOperationPriceQueryHandler : IQueryHandler<GetsFilteredContractorContractDetailOperationPriceQuery, DataResult<List<ContractorContractDetailPrice>>>
{
    private readonly ILogger<GetsFilteredContractorContractDetailOperationPriceQueryHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public GetsFilteredContractorContractDetailOperationPriceQueryHandler(ILogger<GetsFilteredContractorContractDetailOperationPriceQueryHandler> logger, IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetailPrice>>?>> Handle(GetsFilteredContractorContractDetailOperationPriceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsFilteredContractorContractDetailOperationPrice(request.ProjectOperationId, request.OperationInfoId, request.StratDate, request.EndDate,
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
