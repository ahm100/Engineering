using Engineering.Application.Abstractions.Data.ContractorMachineries;
using Engineering.Application.Services.ContractorMachineries.Models.GetFltrByContractorIds;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetFltrByContractorIds;

public class GetFltrByContractorIdsQueryHandler : IQueryHandler<GetFltrByContractorIdsQuery, GetFltrByContractorIdsResponse?>
{
    private readonly IContractorMachineryRepository _repository;
    private readonly ILogger<GetFltrByContractorIdsQueryHandler> _logger;

    public GetFltrByContractorIdsQueryHandler(ILogger<GetFltrByContractorIdsQueryHandler> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetFltrByContractorIdsResponse?>> Handle(GetFltrByContractorIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrByContractorIds(request.ContractorIds, ct);

            return result.Data.Any() ?
                new GetFltrByContractorIdsResponse
                (
                    result.Data,
                    result.RowCount
                ) : Result.Failure<GetFltrByContractorIdsResponse>(ContractorMachineryErrors.FilteredContractorMachineryNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrByContractorIdsResponse>(SharedErrors.UnknownError);
        }
    }
}