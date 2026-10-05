using Engineering.Application.Abstractions.Data.MachineTypes;
using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.InactiveCabinType;

public class InactiveCabinTypeCommandHandler : ICommandHandler<InactiveCabinTypeCommand, CabinType>
{
    private readonly ILogger<InactiveCabinTypeCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public InactiveCabinTypeCommandHandler(
        ILogger<InactiveCabinTypeCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CabinType?>> Handle(InactiveCabinTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCabinTypeById(
                request.Id, ct);
            if (entity is null)
                return Result.Failure<CabinType>(CabinTypeErrors.CabinTypeWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<CabinType>(CabinTypeErrors.IsInactive);

            entity.SetInActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<CabinType>(SharedErrors.UnknownError);
        }
    }
}