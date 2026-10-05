using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.ActiveContractorMachinery;

public class ActiveContractorMachineryCommandHandler : ICommandHandler<ActiveContractorMachineryCommand, ContractorMachinery>
{
    private readonly ILogger<ActiveContractorMachineryCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public ActiveContractorMachineryCommandHandler(ILogger<ActiveContractorMachineryCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(ActiveContractorMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.ContractorMachineryNotFoundWithId);

            if (entity.IsActive == true)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.InValidActivate);

            if (entity.IsDeleted == true)
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.IsDeleted);

            entity.SetActive();

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