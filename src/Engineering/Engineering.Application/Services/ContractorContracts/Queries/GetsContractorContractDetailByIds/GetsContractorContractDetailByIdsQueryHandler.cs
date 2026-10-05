using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractDetailByIds;

public class GetsContractorContractDetailByIdsQueryHandler : IQueryHandler<GetsContractorContractDetailByIdsQuery, DataResult<List<ContractorContractDetail>>>
{
    private readonly ILogger<GetsContractorContractDetailByIdsQueryHandler> _logger;
    private readonly IContractorContractDetailRepository _repository;

    public GetsContractorContractDetailByIdsQueryHandler(
        ILogger<GetsContractorContractDetailByIdsQueryHandler> logger,
        IContractorContractDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractDetail>>?>> Handle(GetsContractorContractDetailByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractDetailByIds(
                request.Ids,
                ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractDetail>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractDetail>>>(SharedErrors.UnknownError);
        }
    }
}
