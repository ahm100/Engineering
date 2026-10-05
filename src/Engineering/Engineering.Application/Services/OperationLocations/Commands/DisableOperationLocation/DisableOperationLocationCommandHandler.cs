using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.DisableOperationLocation;

public class DisableOperationLocationCommandHandler : ICommandHandler<DisableOperationLocationCommand, OperationLocation>
{
    private readonly ILogger<DisableOperationLocationCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public DisableOperationLocationCommandHandler(ILogger<DisableOperationLocationCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(DisableOperationLocationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetOperationLocationById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationLocation>(OperationLocationErrors.OperationLocationWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationLocation>(OperationLocationErrors.IsDeleted);
            if (entity.Children.Count > 0)
                return Result.Failure<OperationLocation>(OperationLocationErrors.CanNotDeleteBecauseOfChildren);
            if (entity.ProjectOperationDetails.Count > 0)
                return Result.Failure<OperationLocation>(OperationLocationErrors.CanNotDeleteBecauseOfProjOpDetail);

            entity.SetIsDeleted();

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