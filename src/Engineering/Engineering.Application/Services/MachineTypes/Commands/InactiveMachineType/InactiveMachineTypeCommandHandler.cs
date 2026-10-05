using Engineering.Application.Abstractions.Data.MachineTypes;
using MachineType = Engineering.Domain.Entities.MachineTypes.MachineType;

namespace Engineering.Application.Services.MachineTypes.Commands.InactiveMachineType;

public class InactiveMachineTypeCommandHandler : ICommandHandler<InactiveMachineTypeCommand, MachineType>
{
    private readonly ILogger<InactiveMachineTypeCommand> _logger;
    private readonly IMachineTypeRepository _repository;

    public InactiveMachineTypeCommandHandler(ILogger<InactiveMachineTypeCommand> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(InactiveMachineTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineType>(MachineTypeErrors.MachineTypeWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<MachineType>(MachineTypeErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineType>(MachineTypeErrors.IsDeleted);

            entity.SetInActive();
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