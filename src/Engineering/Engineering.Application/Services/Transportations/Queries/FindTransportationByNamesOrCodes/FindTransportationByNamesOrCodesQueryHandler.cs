using Engineering.Application.Abstractions.Data.Transportations;

namespace Engineering.Application.Services.Transportations.Queries.FindTransportationByNamesOrCodes;

public class FindTransportationByNamesOrCodesQueryHandler : IQueryHandler<FindTransportationByNamesOrCodesQuery, bool>
{
    private readonly ILogger<FindTransportationByNamesOrCodesQueryHandler> _logger;
    private readonly ITransportationRepository _repository;

    public FindTransportationByNamesOrCodesQueryHandler(ILogger<FindTransportationByNamesOrCodesQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool>> Handle(FindTransportationByNamesOrCodesQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindTransportationByNamesOrCodes(request.Names, request.Codes, request.CompanyId, ct);
            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool>(SharedErrors.UnknownError);
        }
    }
}
