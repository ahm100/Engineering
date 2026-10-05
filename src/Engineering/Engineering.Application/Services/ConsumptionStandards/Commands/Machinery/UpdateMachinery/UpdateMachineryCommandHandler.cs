using Engineering.Application.Abstractions.Data.OperationInfos.ConsumptionStandards;
using ConsumptionStandardMachinery = Engineering.Domain.Entities.OperationInfos.ConsumptionStandards.ConsumptionStandardMachinery;

namespace Engineering.Application.Services.ConsumptionStandards.Commands.Machinery.UpdateMachinery;

public class UpdateMachineryCommandHandler : ICommandHandler<UpdateMachineryCommand, ConsumptionStandardMachinery>
{
    private readonly ILogger<UpdateMachineryCommand> _logger;
    private readonly IConsumptionStandardMachineryRepository _repository;

    public UpdateMachineryCommandHandler(ILogger<UpdateMachineryCommand> logger, IConsumptionStandardMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ConsumptionStandardMachinery?>> Handle(UpdateMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ConsumptionStandardMachinery>(MachineryStandardErrors.MachineryWithIdNotFound);

            entity.SetMachineryEntity(request.Machinery);
            entity.SetMachineryNumber(request.MachineryNumber);
            entity.SetUnusedPercentage(request.UnusedPercentage);
            entity.SetTimeSpant(request.TimeSpant);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ConsumptionStandardMachinery>(SharedErrors.UnknownError);
        }
    }
}