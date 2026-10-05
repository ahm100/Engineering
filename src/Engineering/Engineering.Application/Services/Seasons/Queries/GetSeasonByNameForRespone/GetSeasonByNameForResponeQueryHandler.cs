using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetSeasonByName;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Org.BouncyCastle.Crypto;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByNameForRespone;

public class GetSeasonByNameForResponeQueryHandler : IQueryHandler<GetSeasonByNameForResponeQuery, GetSeasonByNameResponse?>
{
    private readonly ILogger<GetSeasonByNameForResponeQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByNameForResponeQueryHandler(
        ILogger<GetSeasonByNameForResponeQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetSeasonByNameResponse?>> Handle(
        GetSeasonByNameForResponeQuery request, CT ct)
    {
        try
        {
            var seasonResponse = await _repository.FindByNameForResponse(request.SeasonName, null, request.BranchId, ct);
            return seasonResponse ?? Result.Failure<GetSeasonByNameResponse>(SeasonErrors.SeasonWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetSeasonByNameResponse>(SharedErrors.UnknownError)!;
        }
    }
}
