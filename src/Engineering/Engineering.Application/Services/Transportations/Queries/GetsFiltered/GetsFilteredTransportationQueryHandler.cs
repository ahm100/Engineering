using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsFiltered;

public class GetsFilteredTransportationQueryHandler : IQueryHandler<GetsFilteredTransportationQuery, DataResult<List<Transportation>>>
{
    private readonly ITransportationRepository _repository;
    private readonly ILogger<GetsFilteredTransportationQueryHandler> _logger;

    public GetsFilteredTransportationQueryHandler(ILogger<GetsFilteredTransportationQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<Transportation>>?>> Handle(GetsFilteredTransportationQuery request, CT ct)
    {
        try
        {
            var items = await _repository.GetsFilteredTransportation(request.Ids, request.FilterData, request.TransportationCode, request.TransportationName, request.IsPassenger, request.IsActive,
                request.CompanyId, request.OrderBy, request.PageIndex, request.PageSize, ct);

            return items.Data.Any() ?
                new DataResult<List<Transportation>>
                {
                    Data = items.Data,
                    RowCount = items.RowCount
                } : Result.Failure<DataResult<List<Transportation>>>(TransportationErrors.FilteredTransportationNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<Transportation>>>(SharedErrors.UnknownError);
        }
    }
}