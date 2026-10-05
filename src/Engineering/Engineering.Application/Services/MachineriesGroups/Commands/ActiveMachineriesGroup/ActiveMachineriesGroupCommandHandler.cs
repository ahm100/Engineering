using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.ActiveMachineriesGroup;

public class ActiveMachineriesGroupCommandHandler : ICommandHandler<ActiveMachineriesGroupCommand, MachineriesGroup>
{
    private readonly ILogger<ActiveMachineriesGroupCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public ActiveMachineriesGroupCommandHandler(ILogger<ActiveMachineriesGroupCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(ActiveMachineriesGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.IsDeleted);

            entity.SetActive();

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