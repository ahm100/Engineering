using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.DisableContractorMachinery;

public class DisableContractorMachineryCommandHandler : ICommandHandler<DisableContractorMachineryCommand, ContractorMachinery>
{
    private readonly ILogger<DisableContractorMachineryCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public DisableContractorMachineryCommandHandler(ILogger<DisableContractorMachineryCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(DisableContractorMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetContractorMachineryForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.ContractorMachineryNotFoundWithId);
            if (entity.IsDeleted == true)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.IsDeleted);

            entity.SetIsDeleted();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ContractorMachinery>(SharedErrors.UnknownError);
        }
    }
}