using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorContractHeader;

public class GetsDraftableContractorContractHeaderQueryHandler : IQueryHandler<GetsDraftableContractorContractHeaderQuery, DataResult<List<GetsDraftableContractorContractHeaderModel>>>
{
    private readonly ILogger<GetsDraftableContractorContractHeaderQueryHandler> _logger;
    private readonly IContractorContractHeaderRepository _repository;

    public GetsDraftableContractorContractHeaderQueryHandler(
        ILogger<GetsDraftableContractorContractHeaderQueryHandler> logger,
        IContractorContractHeaderRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsDraftableContractorContractHeaderModel>>?>> Handle(GetsDraftableContractorContractHeaderQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDraftableContractorContractHeader(
                request.ContractorId,
                request.ProjectId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDraftableContractorContractHeaderModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDraftableContractorContractHeaderModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDraftableContractorContractHeaderModel>>>(SharedErrors.UnknownError);
        }
    }
}
