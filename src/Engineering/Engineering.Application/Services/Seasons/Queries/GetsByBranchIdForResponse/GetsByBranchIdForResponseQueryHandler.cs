using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.SeasonModels;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetsByBranchIdForResponse;

public class GetsByBranchIdForResponseQueryHandler : IQueryHandler<GetsByBranchIdForResponseQuery, DataResult<List<GetsByBranchIdModel>>?>
{
    private readonly ILogger<GetsByBranchIdForResponseQuery> _logger;
    private readonly ISeasonRepository _repository;

    public GetsByBranchIdForResponseQueryHandler(
        ILogger<GetsByBranchIdForResponseQuery> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsByBranchIdModel>>?>> Handle(
        GetsByBranchIdForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetByBranchIdForResponse(request.BranchId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<GetsByBranchIdModel>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<GetsByBranchIdModel>>>(SeasonErrors.BranchChildNotFound)!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<GetsByBranchIdModel>>>(SharedErrors.UnknownError)!;
        }
    }
}
