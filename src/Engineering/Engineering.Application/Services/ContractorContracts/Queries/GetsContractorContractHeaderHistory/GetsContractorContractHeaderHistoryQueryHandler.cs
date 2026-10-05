using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderHistory;

public class GetsContractorContractHeaderHistoryQueryHandler : IQueryHandler<GetsContractorContractHeaderHistoryQuery, DataResult<List<ContractorContractHeaderHistory>>>
{
    private readonly ILogger<GetsContractorContractHeaderHistoryQueryHandler> _logger;
    private readonly IContractorContractHeaderHistoryRepository _repository;

    public GetsContractorContractHeaderHistoryQueryHandler(ILogger<GetsContractorContractHeaderHistoryQueryHandler> logger,
                                                    IContractorContractHeaderHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractHeaderHistory>>?>> Handle(GetsContractorContractHeaderHistoryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractHeaderHistory(request.ContractorContractId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractHeaderHistory>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractHeaderHistory>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractHeaderHistory>>>(SharedErrors.UnknownError);
        }
    }
}
