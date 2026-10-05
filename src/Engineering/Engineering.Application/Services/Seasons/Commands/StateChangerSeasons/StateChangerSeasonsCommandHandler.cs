using Engineering.Application.Abstractions.Data.Seasons;

namespace Engineering.Application.Services.Seasons.Commands.StateChangerSeasons;

public class StateChangerSeasonsCommandHandler : ICommandHandler<StateChangerSeasonsCommand, bool?>
{
    private readonly ILogger<StateChangerSeasonsCommand> _logger;
    private readonly ISeasonRepository _repository;

    public StateChangerSeasonsCommandHandler(ILogger<StateChangerSeasonsCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(StateChangerSeasonsCommand request, CT ct)
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
                        item.SetDeactivate();
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