using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.ActiveOperationLocation;

public class ActiveOperationLocationCommandHandler : ICommandHandler<ActiveOperationLocationCommand, OperationLocation>
{
    private readonly ILogger<ActiveOperationLocationCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public ActiveOperationLocationCommandHandler(ILogger<ActiveOperationLocationCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(ActiveOperationLocationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationLocation>(OperationLocationErrors.OperationLocationWithIdNotFound);
            if (entity.IsActive == true)
                return Result.Failure<OperationLocation>(OperationLocationErrors.IsActive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationLocation>(OperationLocationErrors.IsDeleted);

            entity.SetActive();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation>(SharedErrors.UnknownError);
        }
    }
}