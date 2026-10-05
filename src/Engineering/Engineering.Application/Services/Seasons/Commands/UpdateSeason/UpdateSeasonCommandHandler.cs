using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.UpdateSeason;

public class UpdateSeasonCommandHandler : ICommandHandler<UpdateSeasonCommand, Season>
{
    private readonly ILogger<UpdateSeasonCommand> _logger;
    private readonly ISeasonRepository _repository;

    public UpdateSeasonCommandHandler(ILogger<UpdateSeasonCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(UpdateSeasonCommand request, CT ct)
    {
        try
        {
            var seasonEntity = await _repository.FindById(request.Id, ct);
            if (seasonEntity is null)
            {
                return Result.Failure<Season>(SeasonErrors.SeasonWithIdNotFound);
            }

            seasonEntity.SetBranch(request.Branch);
            seasonEntity.SetName(request.SeasonName);
            seasonEntity.SetCode(request.SeasonCode);
            seasonEntity.SetCompanyId(request.CompanyId);
            if (request.IsActive != seasonEntity.IsActive)
            {
                if (request.IsActive == true)
                    seasonEntity.SetActive();
                else
                    seasonEntity.SetDeactivate();
            }
            await _repository.Update(seasonEntity);

            return seasonEntity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}