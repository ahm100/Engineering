using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonsByBranchId;

public class GetSeasonsByBranchIdQueryHandler : IQueryHandler<GetSeasonsByBranchIdQuery, List<Season>?>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetSeasonsByBranchIdQueryHandler> _logger;

    public GetSeasonsByBranchIdQueryHandler(ILogger<GetSeasonsByBranchIdQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Season>?>> Handle(GetSeasonsByBranchIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByBranchId(request.BranchId, request.CompanyId, ct);
            if (result is null)
                return Result.Failure<List<Season>>(SeasonErrors.SeasonWithIdNotFound)!;
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Season>>(SharedErrors.UnknownError)!;
        }
    }
}