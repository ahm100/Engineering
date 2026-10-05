using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.DisableMachineriesGroup;

public class DisableMachineriesGroupCommandHandler : ICommandHandler<DisableMachineriesGroupCommand, MachineriesGroup>
{
    private readonly ILogger<DisableMachineriesGroupCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public DisableMachineriesGroupCommandHandler(ILogger<DisableMachineriesGroupCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(DisableMachineriesGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetMachineriesGroupForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.IsDeleted);
            if (entity.Machineries.Any(b => b.ConsumptionStandardMachineries.Count > 0))
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.CanNotDeleteBecauseOfStandards);
            if (entity.Machineries.Any(b => b.ConsumableVolumeMachineries.Count > 0))
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.CanNotDeleteBecauseOfVolumes);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineriesGroup>(SharedErrors.UnknownError);
        }
    }
}