using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetSeasonByCode;
using MathNet.Numerics.Interpolation;
using System.Runtime.CompilerServices;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByCodeForResponse;

public class GetSeasonByCodeForResponseQueryHandler : IQueryHandler<GetSeasonByCodeForResponseQuery, GetSeasonByCodeResponse?>
{
    private readonly ILogger<GetSeasonByCodeForResponseQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByCodeForResponseQueryHandler(
        ILogger<GetSeasonByCodeForResponseQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetSeasonByCodeResponse?>> Handle(
        GetSeasonByCodeForResponseQuery request, CT ct)
    {
        try
        {
            var seasonResponse = await _repository.FindByCodeForResponse(request.SeasonCode, request.BranchId, null, ct);
            return seasonResponse ?? Result.Failure<GetSeasonByCodeResponse>(SeasonErrors.SeasonWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetSeasonByCodeResponse>(SharedErrors.UnknownError);
        }
    }
}
