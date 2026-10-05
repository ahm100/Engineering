using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeaderByIds;

public class GetsContractorContractHeaderByIdsQueryHandler : IQueryHandler<GetsContractorContractHeaderByIdsQuery, DataResult<List<ContractorContractHeader>>>
{
    private readonly ILogger<GetsContractorContractHeaderByIdsQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetsContractorContractHeaderByIdsQueryHandler(
        ILogger<GetsContractorContractHeaderByIdsQueryHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractHeader>>?>> Handle(GetsContractorContractHeaderByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractHeaderByIds(request.Ids, 1, request.Ids.Count, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractHeader>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractHeader>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractHeader>>>(SharedErrors.UnknownError);
        }
    }
}
