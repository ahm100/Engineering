using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonById;

public class GetSeasonByIdQueryHandler : IQueryHandler<GetSeasonByIdQuery, Season>
{
    private readonly ILogger<GetSeasonByIdQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByIdQueryHandler(ILogger<GetSeasonByIdQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Season?>> Handle(GetSeasonByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByIdWithBranch(request.Id, ct);

            return result ?? Result.Failure<Season>(SeasonErrors.SeasonWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Season>(SharedErrors.UnknownError);
        }
    }
}