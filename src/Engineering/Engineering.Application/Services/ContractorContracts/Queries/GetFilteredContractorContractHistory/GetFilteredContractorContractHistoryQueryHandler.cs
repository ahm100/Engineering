using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetFilteredContractorContractHistory;

public class GetContractorContractHistoryQueryHandler : IQueryHandler<GetContractorContractHistoryQuery, DataResult<List<ContractorContractHistory>>>
{
    private readonly ILogger<GetContractorContractHistoryQueryHandler> _logger;
    private readonly IContractorContractHistoryRepository _repository;

    public GetContractorContractHistoryQueryHandler(ILogger<GetContractorContractHistoryQueryHandler> logger,
                                                    IContractorContractHistoryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractHistory>>?>> Handle(GetContractorContractHistoryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFilteredContractorContractHistory(request.ContractorContractId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<ContractorContractHistory>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ContractorContractHistory>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ContractorContractHistory>>>(SharedErrors.UnknownError);
        }
    }
}
