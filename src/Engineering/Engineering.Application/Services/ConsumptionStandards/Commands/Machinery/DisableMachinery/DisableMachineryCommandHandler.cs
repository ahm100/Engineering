using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.DisableMachinery;

public class DisableMachineryCommandHandler : ICommandHandler<DisableMachineryCommand, ConsumptionStandardMachinery>
{
    private readonly ILogger<DisableMachineryCommand> _logger;
    private readonly IConsumptionStandardMachineryRepository _repository;

    public DisableMachineryCommandHandler(ILogger<DisableMachineryCommand> logger, IConsumptionStandardMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardMachinery?>> Handle(DisableMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardMachinery>(MachineryStandardErrors.MachineryWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<ConsumptionStandardMachinery>(MachineryStandardErrors.IsDeleted);

            entity.SetIsDeleted();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ConsumptionStandardMachinery>(SharedErrors.UnknownError);
        }
    }
}