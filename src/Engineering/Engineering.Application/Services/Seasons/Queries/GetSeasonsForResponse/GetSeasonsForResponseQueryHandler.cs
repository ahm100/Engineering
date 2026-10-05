using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.SeasonModels;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonsForResponse;

public class GetSeasonsForResponseQueryHandler : IQueryHandler<GetSeasonsForResponseQuery, DataResult<List<GetSeasonsModel>?>?>
{
    private readonly ILogger<GetSeasonsForResponseQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonsForResponseQueryHandler(
        ILogger<GetSeasonsForResponseQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetSeasonsModel>?>?>> Handle(
        GetSeasonsForResponseQuery request, CancellationToken ct)
    {
        try
        {
            var result = await _repository.GetSeasonsForResponse(
                request.Ids, request.FilterData, request.BranchId,
                request.CategoryId, request.SeasonCode, request.SeasonName,
                request.IsActive, request.OrderBy, request.CompanyId,
                request.PageIndex, request.PageSize, ct);

            if (result.Data is null || !result.Data.Any())
                return Result.Failure<DataResult<List<GetSeasonsModel>>>(SeasonErrors.FilteredSeasonNotFound)!;

            return Result.Success(new DataResult<List<GetSeasonsModel>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            })!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting seasons for response");
            return Result.Failure<DataResult<List<GetSeasonsModel>>>(SharedErrors.UnknownError)!;
        }
    }
}