using Engineering.Application.Abstractions.Data.Machineries;
using MachineriesGroup = Engineering.Domain.Entities.Machineries.MachineriesGroup;

namespace Engineering.Application.Services.MachineriesGroups.Commands.UpdateMachineriesGroup;

public class UpdateMachineriesGroupCommandHandler : ICommandHandler<UpdateMachineriesGroupCommand, MachineriesGroup>
{
    private readonly ILogger<UpdateMachineriesGroupCommand> _logger;
    private readonly IMachineriesGroupRepository _repository;

    public UpdateMachineriesGroupCommandHandler(ILogger<UpdateMachineriesGroupCommand> logger, IMachineriesGroupRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineriesGroup?>> Handle(UpdateMachineriesGroupCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineriesGroup>(MachineriesGroupErrors.MachineriesGroupWithIdNotFound);

            entity.SetName(request.GroupName);
            entity.SetCode(request.GroupCode);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetDeactivate();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<MachineriesGroup>(SharedErrors.UnknownError);
        }
    }
}