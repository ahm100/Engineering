using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetByNames;

public class GetByNamesQueryHandler : IQueryHandler<GetByNamesQuery, List<Season>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetByNamesQueryHandler> _logger;

    public GetByNamesQueryHandler(ILogger<GetByNamesQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Season>?>> Handle(GetByNamesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByNames(request.Names, ct);
            if (result is null)
                return Result.Failure<List<Season>?>(SharedErrors.UnknownError);
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Season>?>(SharedErrors.UnknownError);
        }
    }
}