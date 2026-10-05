using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.DisableSeason;

public class DisableSeasonCommandHandler : ICommandHandler<DisableSeasonCommand, Season>
{
    private readonly ILogger<DisableSeasonCommand> _logger;
    private readonly ISeasonRepository _repository;

    public DisableSeasonCommandHandler(ILogger<DisableSeasonCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(DisableSeasonCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<Season>(SeasonErrors.SeasonWithIdNotFound);
            if (entity.IsDeleted == true)
                return Result.Failure<Season>(SeasonErrors.IsDeleted);
            if (entity.OperationInfoSeasons.Count > 0)
                return Result.Failure<Season>(SeasonErrors.CanNottDelete);

            entity.SoftDelete();

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}