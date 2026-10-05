using Engineering.Application.Abstractions.Data.MachineTypes;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.ActiveMachineType;

public class ActiveMachineTypeCommandHandler : ICommandHandler<ActiveMachineTypeCommand, MachineType>
{
    private readonly ILogger<ActiveMachineTypeCommand> _logger;
    private readonly IMachineTypeRepository _repository;

    public ActiveMachineTypeCommandHandler(ILogger<ActiveMachineTypeCommand> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(ActiveMachineTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineType>(MachineTypeErrors.MachineTypeWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<MachineType>(MachineTypeErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineType>(MachineTypeErrors.IsDeleted);

            entity.SetActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MachineType>(SharedErrors.UnknownError);
        }
    }
}