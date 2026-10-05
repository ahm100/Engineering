using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Update;

public class UpdateTripCommandHandler : ICommandHandler<UpdateTripCommand, Trip>
{
    private readonly ILogger<UpdateTripCommand> _logger;
    private readonly ITripRepository _repository;

    public UpdateTripCommandHandler(ILogger<UpdateTripCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(UpdateTripCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<Trip>(TripErrors.TripWithIdNotFound);

            if (entity.IsLock != null && entity.IsLock == true)
                return Result.Failure<Trip>(TripErrors.IsLockData);

            entity.SetName(request.TripName);
            entity.SetCode(request.TripCode);
            entity.SetCompanyId(request.CompanyId);
            if (request.IsActive != entity.IsActive)
            {
                if (request.IsActive == true)
                    entity.SetActive();
                else
                    entity.SetInActive();
            }

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Trip>(SharedErrors.UnknownError);
        }
    }
}