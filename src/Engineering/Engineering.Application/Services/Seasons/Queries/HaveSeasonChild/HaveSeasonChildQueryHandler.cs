using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.HaveSeasonChild;

public class HaveSeasonChildQueryHandler : IQueryHandler<HaveSeasonChildQuery, Season>
{
    private readonly ILogger<HaveSeasonChildQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public HaveSeasonChildQueryHandler(ILogger<HaveSeasonChildQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(HaveSeasonChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveSeasonChild(request.Id, ct);

            return result ?? Result.Failure<Season>(SharedErrors.ItemNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}