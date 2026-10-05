using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCode;

public class GetSeasonByCodeQueryHandler : IQueryHandler<GetSeasonByCodeQuery, Season>
{
    private readonly ILogger<GetSeasonByCodeQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByCodeQueryHandler(ILogger<GetSeasonByCodeQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(GetSeasonByCodeQuery request, CT ct)
    {
        try
        {
            var seasonResponse = await _repository.FindByCode(request.SeasonCode, request.BranchId, request.CompanyId, ct);
            return seasonResponse ?? Result.Failure<Season>(SeasonErrors.SeasonWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}