using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Commands.CreateSeason;

public class CreateSeasonCommandHandler : ICommandHandler<CreateSeasonCommand, Season>
{
    private readonly ILogger<CreateSeasonCommand> _logger;
    private readonly ISeasonRepository _repository;

    public CreateSeasonCommandHandler(ILogger<CreateSeasonCommand> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(CreateSeasonCommand request, CT ct)
    {
        try
        {
            var newSeason = new Season(request.Branch,
                request.SeasonName,
                request.SeasonCode,
                request.IsActive,
                request.CompanyId);
            var result = await _repository.Create(newSeason, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}