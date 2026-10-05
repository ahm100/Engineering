using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.InactiveSeason;

public class InactiveSeasonCommandHandler : ICommandHandler<InactiveSeasonCommand, Season>
{
    private readonly ILogger<InactiveSeasonCommand> _logger;
    private readonly ISeasonRepository _repository;

    public InactiveSeasonCommandHandler(ILogger<InactiveSeasonCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(InactiveSeasonCommand request, CT ct)
    {
        try
        {
            var seasonEntity = await _repository.FindById(request.Id, ct);
            if (seasonEntity is null)
            {
                return Result.Failure<Season>(SeasonErrors.SeasonWithIdNotFound);
            }
            if (seasonEntity.IsActive == false)
            {
                return Result.Failure<Season>(SeasonErrors.IsInactive);
            }
            if (seasonEntity.IsDeleted == true)
            {
                return Result.Failure<Season>(SeasonErrors.IsDeleted);
            }

            seasonEntity.SetDeactivate();

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