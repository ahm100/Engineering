using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.ActiveSeason;

public class ActiveSeasonCommandHandler : ICommandHandler<ActiveSeasonCommand, Season>
{
    private readonly ILogger<ActiveSeasonCommand> _logger;
    private readonly ISeasonRepository _repository;

    public ActiveSeasonCommandHandler(ILogger<ActiveSeasonCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(ActiveSeasonCommand request, CT ct)
    {
        try
        {
            var seasonEntity = await _repository.FindById(request.Id, ct);
            if (seasonEntity is null)
            {
                return Result.Failure<Season>(SeasonErrors.SeasonWithIdNotFound);
            }
            if (seasonEntity.IsActive == true)
            {
                return Result.Failure<Season>(SeasonErrors.IsActive);
            }
            if (seasonEntity.IsDeleted == true)
            {
                return Result.Failure<Season>(SeasonErrors.IsDeleted);
            }

            seasonEntity.SetActive();

            await _repository.Update(seasonEntity);

            return seasonEntity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}