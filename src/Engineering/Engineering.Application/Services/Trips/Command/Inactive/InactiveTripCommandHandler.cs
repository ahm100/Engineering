using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Inactive;

public class InactiveTripCommandHandler : ICommandHandler<InactiveTripCommand, Trip>
{
    private readonly ILogger<InactiveTripCommand> _logger;
    private readonly ITripRepository _repository;

    public InactiveTripCommandHandler(ILogger<InactiveTripCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(InactiveTripCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Trip>(TripErrors.TripWithIdNotFound);
            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Trip>(TripErrors.IsLockData);
            if (entity.IsActive == false)
                return Result.Failure<Trip>(TripErrors.IsInactive);
            if (entity.IsDeleted == true)
                return Result.Failure<Trip>(TripErrors.IsDeleted);

            entity.SetInActive();
            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Trip>(SharedErrors.UnknownError);
        }
    }
}