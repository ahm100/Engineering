using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;

namespace Engineering.Application.Services.ContractorContracts.Queries.GetCostCentersMostPaidCC;

public class GetCostCentersMostPaidCCQueryHandler : IQueryHandler<GetCostCentersMostPaidCCQuery, GetCostCentersMostPaidCCResponse?>
{
    private readonly ILogger<GetCostCentersMostPaidCCQueryHandler> _logger;
    private readonly IContractorContractRepository _repository;

    public GetCostCentersMostPaidCCQueryHandler(
        ILogger<GetCostCentersMostPaidCCQueryHandler> logger,
        IContractorContractRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetCostCentersMostPaidCCResponse?>> Handle(GetCostCentersMostPaidCCQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetCostCentersMostPaidCC(request.CompanyId, ct);
            if (result is null)
                return Result.Failure<GetCostCentersMostPaidCCResponse>(ContractorContractErrors.InValidContractorContractId);

            return result.Any() ?
                new GetCostCentersMostPaidCCResponse(result, result.Count) :
                Result.Failure<GetCostCentersMostPaidCCResponse?>(SharedErrors.UnknownError);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetCostCentersMostPaidCCResponse?>(SharedErrors.UnknownError);
        }
    }
}
