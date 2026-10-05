using Engineering.Application.Abstractions.Data.ContractorMachineries;
using Engineering.Domain.Entities.ContractorMachineries;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetDuplicateContractorMachinery;

public class GetDuplicateContractorMachineryQueryHandler : IQueryHandler<GetDuplicateContractorMachineryQuery, ContractorMachinery?>
{
    private readonly ILogger<GetDuplicateContractorMachineryQueryHandler> _logger;
    private readonly IContractorMachineryRepository _repository;

    public GetDuplicateContractorMachineryQueryHandler(ILogger<GetDuplicateContractorMachineryQueryHandler> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(GetDuplicateContractorMachineryQuery request, CT ct)
    {
        try
        {
            var result = await _repository.IsDuplicate(request.ContractorId, request.MachineryId, request.Unit, request.CompanyId, ct);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorMachinery?>(SharedErrors.UnknownError);
        }
    }
}