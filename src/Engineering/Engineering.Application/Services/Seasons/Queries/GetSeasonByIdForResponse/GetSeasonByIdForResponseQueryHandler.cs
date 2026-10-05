using Engineering.Application.Abstractions.Data.Seasons;
using Engineering.Application.Services.Seasons.Models.GetSeasonById;
using Engineering.Domain.Entities.Seasons;
using System.Runtime.InteropServices;

namespace Engineering.Application.Services.Seasons.Queries.GetSeasonByIdForResponse;

public class GetSeasonByIdForResponseQueryHandler : IQueryHandler<GetSeasonByIdForResponseQuery, GetSeasonByIdResponse?>
{
    private readonly ILogger<GetSeasonByIdForResponseQueryHandler> _logger;
    private readonly ISeasonRepository _repository;

    public GetSeasonByIdForResponseQueryHandler(
        ILogger<GetSeasonByIdForResponseQueryHandler> logger,
        ISeasonRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetSeasonByIdResponse?>> Handle(
        GetSeasonByIdForResponseQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByIdWithBranchForResponse(request.Id, ct);

            return result ?? Result.Failure<GetSeasonByIdResponse>(SeasonErrors.SeasonWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetSeasonByIdResponse>(SharedErrors.UnknownError);
        }
    }
}
