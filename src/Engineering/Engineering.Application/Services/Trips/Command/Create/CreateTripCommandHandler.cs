using Engineering.Application.Abstractions.Data.Trips;
using Trip = Engineering.Domain.Entities.Trips.Trip;

namespace Engineering.Application.Services.Trips.Commands.Create;

public class CreateTripCommandHandler : ICommandHandler<CreateTripCommand, Trip?>
{
    private readonly ILogger<CreateTripCommand> _logger;
    private readonly ITripRepository _repository;

    public CreateTripCommandHandler(ILogger<CreateTripCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Trip?>> Handle(CreateTripCommand request, CT ct)
    {
        try
        {
            var entity = new Trip(request.TripName, request.TripCode, request.IsActive, request.CompanyId);

            return await _repository.Create(entity, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Trip?>(SharedErrors.UnknownError);
        }
    }
}