using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsContractorContractHeader;

public class GetsContractorContractHeaderQueryHandler : IQueryHandler<GetsContractorContractHeaderQuery, DataResult<List<ContractorContractHeader>>>
{
    private readonly ILogger<GetsContractorContractHeaderQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetsContractorContractHeaderQueryHandler(ILogger<GetsContractorContractHeaderQueryHandler> logger,
                                                      IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ContractorContractHeader>>?>> Handle(GetsContractorContractHeaderQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsContractorContractHeader(request.ContractorId, request.ProjectId, request.PageIndex, request.PageSize, ct);

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
