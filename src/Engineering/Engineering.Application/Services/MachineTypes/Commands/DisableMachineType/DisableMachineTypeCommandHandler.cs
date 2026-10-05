using Engineering.Application.Abstractions.Data.MachineTypes;
using Engineering.Domain.Entities.MachineTypes;
namespace Engineering.Application.Services.MachineTypes.Commands.DisableMachineType;

public class DisableMachineTypeCommandHandler : ICommandHandler<DisableMachineTypeCommand, MachineType>
{
    private readonly ILogger<DisableMachineTypeCommand> _logger;
    private readonly IMachineTypeRepository _repository;

    public DisableMachineTypeCommandHandler(ILogger<DisableMachineTypeCommand> logger, IMachineTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<MachineType?>> Handle(DisableMachineTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<MachineType>(MachineTypeErrors.MachineTypeNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<MachineType>(MachineTypeErrors.IsDeleted);
            if (entity.TransportationRequests.Count > 0)
                return Result.Failure<MachineType>(MachineTypeErrors.CanNottDeleteBecauseOfTransportationRequest);
            if (entity.ShippingCosts.Count > 0)
                return Result.Failure<MachineType>(MachineTypeErrors.CanNotDeleteForShippingcost);

            entity.SetIsDeleted();

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