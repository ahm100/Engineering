using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailCostOver;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailCostOver;

public class GetsContractorContractDetailCostOverQueryHandler : IQueryHandler<GetsContractorContractDetailCostOverQuery, DataResult<List<GetsContractorContractDetailCostOverModel>>>
{
    private readonly ILogger<GetsContractorContractDetailCostOverQueryHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public GetsContractorContractDetailCostOverQueryHandler(
        ILogger<GetsContractorContractDetailCostOverQueryHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsContractorContractDetailCostOverModel>>?>> Handle(GetsContractorContractDetailCostOverQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractDetailCostOver(
                request.ContractorContractHedearId,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsContractorContractDetailCostOverModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsContractorContractDetailCostOverModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorContractDetailCostOverModel>>>(SharedErrors.UnknownError);
        }
    }
}
