using Engineering.Application.Abstractions.Data.ContractorMachineries;
using ContractorMachinery = Engineering.Domain.Entities.ContractorMachineries.ContractorMachinery;

namespace Engineering.Application.Services.ContractorMachineries.Commands.InactiveContractorMachinery;

public class InactiveContractorMachineryCommandHandler : ICommandHandler<InactiveContractorMachineryCommand, ContractorMachinery>
{
    private readonly ILogger<InactiveContractorMachineryCommand> _logger;
    private readonly IContractorMachineryRepository _repository;

    public InactiveContractorMachineryCommandHandler(ILogger<InactiveContractorMachineryCommand> logger, IContractorMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ContractorMachinery?>> Handle(InactiveContractorMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
            {
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.ContractorMachineryNotFoundWithId);
            }
            if (entity.IsActive == false)
            {
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.InValidInActivate);
            }
            if (entity.IsDeleted == true)
            {
                return Result.Failure<ContractorMachinery>(ContractorMachineryErrors.IsDeleted);
            }

            entity.SetInActive();

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