using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractCostOverForCSS;

public class GetsContractorContractCostOverForCSSQueryHandler : IQueryHandler<GetsContractorContractCostOverForCSSQuery, DataResult<List<ContractorContractDetailCostOver>>>
{
    private readonly ILogger<GetsContractorContractCostOverForCSSQueryHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public GetsContractorContractCostOverForCSSQueryHandler(
        ILogger<GetsContractorContractCostOverForCSSQueryHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetailCostOver>>?>> Handle(GetsContractorContractCostOverForCSSQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractCostOverForCSS(
                request.ContractorId,
                request.ProjectId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractDetailCostOver>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractDetailCostOver>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractDetailCostOver>>>(SharedErrors.UnknownError);
        }
    }
}
