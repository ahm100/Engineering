using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.SeasonModels;
using Engineering.Domain.Entities.Seasons;

namespace Engineering.Application.Services.Seasons.Queries.GetActiveSeasonsForResponse;

public class GetActiveSeasonsForResponseQueryHandler : IQueryHandler<GetActiveSeasonsForResponseQuery, DataResult<List<GetsActiveSeasonModel>>?>
{
    private readonly ILogger<GetActiveSeasonsForResponseQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetActiveSeasonsForResponseQueryHandler(
        ILogger<GetActiveSeasonsForResponseQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<GetsActiveSeasonModel>>?>> Handle(
        GetActiveSeasonsForResponseQuery request, CancellationToken ct)
    {
        try
        {
            var result = await _repository.GetActiveSeasonsForResponse(
                request.FilterData, request.BranchId, request.SeasonCode,
                request.SeasonName, request.CompanyId,
                request.PageIndex, request.PageSize, ct);

            if (result.Data is null || !result.Data.Any())
                return Result.Failure<DataResult<List<GetsActiveSeasonModel>>>(
                    SeasonErrors.FilteredSeasonNotFound)!;

            return Result.Success(new DataResult<List<GetsActiveSeasonModel>>
            {
                Data = result.Data,
                RowCount = result.RowCount
            })!;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting active seasons for response");
            return Result.Failure<DataResult<List<GetsActiveSeasonModel>>>(
                SharedErrors.UnknownError)!;
        }
    }
}