using Engineering.Application.Abstractions.Data.Machineries;
using Machinery = Engineering.Domain.Entities.Machineries.Machinery;

namespace Engineering.Application.Services.Machineries.Commands.DisableMachinery;

public class DisableMachineryCommandHandler : ICommandHandler<DisableMachineryCommand, Machinery>
{
    private readonly ILogger<DisableMachineryCommand> _logger;
    private readonly IMachineryRepository _repository;

    public DisableMachineryCommandHandler(ILogger<DisableMachineryCommand> logger, IMachineryRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Machinery?>> Handle(DisableMachineryCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetMachineryForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<Machinery>(MachineryErrors.MachineryWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<Machinery>(MachineryErrors.IsDeleted);
            if (entity.ConsumptionStandardMachineries.Count > 0)
                return Result.Failure<Machinery>(MachineryErrors.CanNotDeleteBecauseOfStandards);
            if (entity.ConsumableVolumeMachineries.Count > 0)
                return Result.Failure<Machinery>(MachineryErrors.CanNotDeleteBecauseOfVolumes);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Machinery>(SharedErrors.UnknownError);
        }
    }
}