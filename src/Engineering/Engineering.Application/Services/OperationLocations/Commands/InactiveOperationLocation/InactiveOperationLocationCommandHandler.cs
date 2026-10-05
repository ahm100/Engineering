using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Commands.InactiveOperationLocation;

public class InactiveOperationLocationCommandHandler : ICommandHandler<InactiveOperationLocationCommand, OperationLocation>
{
    private readonly ILogger<InactiveOperationLocationCommand> _logger;
    private readonly IOperationLocationRepository _repository;

    public InactiveOperationLocationCommandHandler(ILogger<InactiveOperationLocationCommand> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(InactiveOperationLocationCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<OperationLocation>(OperationLocationErrors.OperationLocationWithIdNotFound);
            if (entity.IsActive == false)
                return Result.Failure<OperationLocation>(OperationLocationErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<OperationLocation>(OperationLocationErrors.IsDeleted);

            entity.SetInActive();

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