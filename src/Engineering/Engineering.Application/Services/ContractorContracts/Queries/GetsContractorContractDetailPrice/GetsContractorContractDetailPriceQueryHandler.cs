using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailPrice;

public class GetsContractorContractDetailPriceQueryHandler : IQueryHandler<GetsContractorContractDetailPriceQuery, DataResult<List<GetsContractorContractDetailPriceModel>>>
{
    private readonly ILogger<GetsContractorContractDetailPriceQueryHandler> _logger;
    private readonly IContractorContractDetailPriceRepository _repository;

    public GetsContractorContractDetailPriceQueryHandler(
        ILogger<GetsContractorContractDetailPriceQueryHandler> logger,
        IContractorContractDetailPriceRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsContractorContractDetailPriceModel>>?>> Handle(GetsContractorContractDetailPriceQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractDetailPrice(
                request.ContractorContractHedearId,
                request.StartDate,
                request.EndDate,
                request.FilterData,
                request.OrderBy,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsContractorContractDetailPriceModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsContractorContractDetailPriceModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsContractorContractDetailPriceModel>>>(SharedErrors.UnknownError);
        }
    }
}
