using Engineering.Application.Abstractions.Data.Trips;

namespace Engineering.Application.Services.Trips.Queries.FindTripByNamesOrCodes;

public class FindTripByNamesOrCodesQueryHandler : IQueryHandler<FindTripByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindTripByNamesOrCodesQueryHandler> _logger;
    private readonly ITripRepository _repository;

    public FindTripByNamesOrCodesQueryHandler(ILogger<FindTripByNamesOrCodesQueryHandler> logger, ITripRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindTripByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindTripByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
