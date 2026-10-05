using Engineering.Application.Abstractions.Data.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.FindSeasonByNamesOrCodes;

public class FindSeasonByNamesOrCodesQueryHandler : IQueryHandler<FindSeasonByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindSeasonByNamesOrCodesQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public FindSeasonByNamesOrCodesQueryHandler(ILogger<FindSeasonByNamesOrCodesQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindSeasonByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindSeasonByNamesOrCodes(request.Names, request.Codes, request.BranchId, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
