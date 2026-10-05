using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetsSeasonByBranchIds;

namespace Engineering.Application.Services.Seasons.Queries.GetsSeasonByBranchIdsForResponse;

public class GetsSeasonByBranchIdsForResponseQueryHandler : IQueryHandler<GetsSeasonByBranchIdsForResponseQuery, DataResult<List<GetsSeasonByBranchIdsModel>?>?>
{
    private readonly ILogger<GetsSeasonByBranchIdsForResponseQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetsSeasonByBranchIdsForResponseQueryHandler(
        ILogger<GetsSeasonByBranchIdsForResponseQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsSeasonByBranchIdsModel>?>?>> Handle(
        GetsSeasonByBranchIdsForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsSeasonByBranchIdsForResponse(request.BranchIds, request.FilterData, request.IsActive, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsSeasonByBranchIdsModel>?>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsSeasonByBranchIdsModel>?>>(SeasonErrors.BranchChildNotFound)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsSeasonByBranchIdsModel>>>(SharedErrors.UnknownError)!;
        }
    }
}
