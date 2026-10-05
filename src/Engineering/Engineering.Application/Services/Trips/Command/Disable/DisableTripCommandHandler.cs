using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Disable;

public class DisableTripCommandHandler : ICommandHandler<DisableTripCommand, Trip>
{
    private readonly ILogger<DisableTripCommand> _logger;
    private readonly ITripRepository _repository;

    public DisableTripCommandHandler(ILogger<DisableTripCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(DisableTripCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<Trip>(TripErrors.TripWithIdNotFound);
            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Trip>(TripErrors.IsLockData);
            if (entity.IsDeleted == true)
                return Result.Failure<Trip>(TripErrors.IsDeleted);
            if (entity.TransportationRequests.Any())
                return Result.Failure<Trip>(TripErrors.IsDeletedForTransportationRequest);

            entity.SetIsDeleted();

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