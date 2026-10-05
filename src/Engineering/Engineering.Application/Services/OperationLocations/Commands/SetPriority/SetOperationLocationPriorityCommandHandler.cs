using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.SetPriority;

public class SetOperationLocationPriorityCommandHandler : ICommandHandler<SetOperationLocationPriorityCommand, OperationLocation>
{
    private readonly ILogger<SetOperationLocationPriorityCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public SetOperationLocationPriorityCommandHandler(ILogger<SetOperationLocationPriorityCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(SetOperationLocationPriorityCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationLocation>(OperationLocationErrors.OperationLocationWithIdNotFound);

            entity.SetPriority(request.Priority);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<OperationLocation>(SharedErrors.UnknownError);
        }
    }
}