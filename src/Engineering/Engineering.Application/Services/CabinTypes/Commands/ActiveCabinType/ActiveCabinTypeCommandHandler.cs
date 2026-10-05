using Engineering.Application.Abstractions.Data.MachineTypes;
using CabinType = Engineering.Domain.Entities.MachineTypes.CabinType;

namespace Engineering.Application.Services.CabinTypes.Commands.ActiveCabinType;

public class ActiveCabinTypeCommandHandler : ICommandHandler<ActiveCabinTypeCommand, CabinType>
{
    private readonly ILogger<ActiveCabinTypeCommand> _logger;
    private readonly ICabinTypeRepository _repository;

    public ActiveCabinTypeCommandHandler(
        ILogger<ActiveCabinTypeCommand> logger,
        ICabinTypeRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<CabinType?>> Handle(ActiveCabinTypeCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetCabinTypeById(
                request.Id, ct);
            if (entity is null)
                return Result.Failure<CabinType>(CabinTypeErrors.CabinTypeWithIdNotFound);
            if (entity.IsActive)
                return Result.Failure<CabinType>(CabinTypeErrors.IsActive);

            entity.SetActive();
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