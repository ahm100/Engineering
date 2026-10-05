using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.CreateContractorMachinery;

public class CreateContractorMachineryCommandHandler : ICommandHandler<CreateContractorMachineryCommand, ContractorMachinery?>
{
    private readonly ILogger<CreateContractorMachineryCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public CreateContractorMachineryCommandHandler(ILogger<CreateContractorMachineryCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(CreateContractorMachineryCommand request, CT ct)
    {
        try
        {
            var newContractorMachinery = new ContractorMachinery(request.Machinery, request.ContractorId, request.MachineryPrice, request.CurrencyId,
                request.Unit, request.NumberPlates, request.MachineryIdentifier, request.IsActive, request.Description, request.CompanyId);
            var result = await _repository.Create(newContractorMachinery, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorMachinery?>(SharedErrors.UnknownError);
        }
    }
}