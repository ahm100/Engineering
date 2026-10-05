using Engineering.Application.Abstractions.Data.ContractorContracts;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;

public class GetsDraftableContractorCostOverQueryHandler : IQueryHandler<GetsDraftableContractorCostOverQuery, DataResult<List<GetsDraftableContractorCostOverModel>>>
{
    private readonly ILogger<GetsDraftableContractorCostOverQueryHandler> _logger;
    private readonly IContractorContractDetailCostOverRepository _repository;

    public GetsDraftableContractorCostOverQueryHandler(
        ILogger<GetsDraftableContractorCostOverQueryHandler> logger,
        IContractorContractDetailCostOverRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsDraftableContractorCostOverModel>>?>> Handle(GetsDraftableContractorCostOverQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsDraftableContractorCostOver(
                request.ContractorId,
                request.ProjectId,
                request.PageIndex,
                request.PageSize,
                ct);

            return result.Data.Any() ?
                new DataResult<List<GetsDraftableContractorCostOverModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsDraftableContractorCostOverModel>>>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsDraftableContractorCostOverModel>>>(SharedErrors.UnknownError);
        }
    }
}
