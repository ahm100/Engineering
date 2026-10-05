using Engineering.Application.Abstractions.Data.Trips;

namespace Engineering.Application.Services.Trips.Commands.StateChangerTrips;

public class StateChangerTripsCommandHandler : ICommandHandler<StateChangerTripsCommand, bool?>
{
    private readonly ILogger<StateChangerTripsCommand> _logger;
    private readonly ITripRepository _repository;

    public StateChangerTripsCommandHandler(ILogger<StateChangerTripsCommand> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerTripsCommand request, CT ct)
    {
        try
        {
            if (request.State)
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetActive();
                        await _repository.Update(item);
                    }
                }
            else
                foreach (var item in request.Items)
                {
                    if (item.IsActive != request.State)
                    {
                        item.SetInActive();
                        await _repository.Update(item);
                    }
                }

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}