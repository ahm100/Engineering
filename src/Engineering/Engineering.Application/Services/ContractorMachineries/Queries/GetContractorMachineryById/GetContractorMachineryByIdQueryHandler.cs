using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Queries.GetContractorMachineryById;

public class GetContractorMachineryByIdQueryHandler : IQueryHandler<GetContractorMachineryByIdQuery, ContractorMachinery?>
{
    private readonly ILogger<GetContractorMachineryByIdQueryHandler> _logger;
    private readonly IContractorMachineryRepository _repository;

    public GetContractorMachineryByIdQueryHandler(ILogger<GetContractorMachineryByIdQueryHandler> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(GetContractorMachineryByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.Id, ct);

            return result ?? Result.Failure<ContractorMachinery?>(ContractorMachineryErrors.ContractorMachineryNotFoundWithId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorMachinery?>(SharedErrors.UnknownError);
        }
    }
}