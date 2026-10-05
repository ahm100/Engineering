
using Engineering.Application.Abstractions.Data.Seasons;
using Season = Engineering.Domain.Entities.Seasons.Season;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchId;

public class GetsByBranchIdQueryHandler : IQueryHandler<GetsByBranchIdQuery, DataResult<List<Season>>>
{
    private readonly ISeasonRepository _repository;
    private readonly ILogger<GetsByBranchIdQueryHandler> _logger;

    public GetsByBranchIdQueryHandler(ILogger<GetsByBranchIdQueryHandler> logger, ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Season>>?>> Handle(GetsByBranchIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByBranchId(request.BranchId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Season>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Season>>>(SeasonErrors.BranchChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Season>>>(SharedErrors.UnknownError);
        }
    }
}