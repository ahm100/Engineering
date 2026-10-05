using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostRecentCC;


public class GetCostCentersMostRecentQueryHandler : IQueryHandler<GetCostCentersMostRecentQuery, GetCostCentersMostRecentCCResponse?>
{
    private readonly ILogger<GetCostCentersMostRecentQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetCostCentersMostRecentQueryHandler(
        ILogger<GetCostCentersMostRecentQueryHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCentersMostRecentCCResponse?>> Handle(GetCostCentersMostRecentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCentersMostRecentCC(request.CompanyId, ct);
            if (result is null)
                return Result.Failure<GetCostCentersMostRecentCCResponse>(ContractorContractErrors.InValidContractorContractId);

            return result.Any() ?
                new GetCostCentersMostRecentCCResponse(result, result.Count) :
                Result.Failure<GetCostCentersMostRecentCCResponse?>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCentersMostRecentCCResponse?>(SharedErrors.UnknownError);
        }
    }
}
