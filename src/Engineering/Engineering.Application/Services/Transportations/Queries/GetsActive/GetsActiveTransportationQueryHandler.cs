using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsActive;

public class GetsActiveTransportationQueryHandler : IQueryHandler<GetsActiveTransportationQuery, DataResult<List<Transportation>>>
{
    private readonly ITransportationRepository _repository;
    private readonly ILogger<GetsActiveTransportationQuery> _logger;

    public GetsActiveTransportationQueryHandler(ILogger<GetsActiveTransportationQuery> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Transportation>>?>> Handle(GetsActiveTransportationQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsActiveTransportation(request.FilterData, request.TransportationCode, request.TransportationName, request.CompanyId, request.PageIndex, request.PageSize, ct);

            return result.Data.Any() ?
                new DataResult<List<Transportation>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<Transportation>>>(TransportationErrors.FilteredTransportationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Transportation>>>(SharedErrors.UnknownError);
        }
    }
}