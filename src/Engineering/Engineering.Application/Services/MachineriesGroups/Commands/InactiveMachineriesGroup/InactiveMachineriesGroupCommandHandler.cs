using Engineering.Application.Abstractions.Data.Machineries;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.InactiveMachineriesGroup;

public class InactiveMachineriesGroupCommandHandler : ICommandHandler<InactiveMachineriesGroupCommand, MachineriesGroup>
{
    private readonly ILogger<InactiveMachineriesGroupCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public InactiveMachineriesGroupCommandHandler(ILogger<InactiveMachineriesGroupCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(InactiveMachineriesGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetMachineriesGroupForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.IsDeleted);
            if (entity.Machineries.Any(b => b.ConsumableVolumeMachineries.Any(x => x.ProjectOperationDetail.Status == ProjectOperationDetailStatus.NotStarted ||
                                                                                   x.ProjectOperationDetail.Status == ProjectOperationDetailStatus.Doing ||
                                                                                   x.ProjectOperationDetail.Status == ProjectOperationDetailStatus.Stopped)))
            {
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.CanNotForStatus);
            }

            entity.SetDeactivate();

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