using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByName;

public class GetSeasonByNameQueryHandler : IQueryHandler<GetSeasonByNameQuery, Season>
{
    private readonly ILogger<GetSeasonByNameQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByNameQueryHandler(ILogger<GetSeasonByNameQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(GetSeasonByNameQuery request, CT ct)
    {
        try
        {
            var seasonResponse = await _repository.FindByName(request.SeasonName, request.CompanyId, request.BranchId, ct);
            return seasonResponse ?? Result.Failure<Season>(SeasonErrors.SeasonWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}