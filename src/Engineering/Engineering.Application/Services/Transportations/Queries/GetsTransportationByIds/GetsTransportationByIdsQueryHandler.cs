using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetsTransportationByIds;

public class GetsTransportationByIdsQueryHandler : IQueryHandler<GetsTransportationByIdsQuery, List<Transportation>>
{
    private readonly ITransportationRepository _repository;
    private readonly ILogger<GetsTransportationByIdsQuery> _logger;

    public GetsTransportationByIdsQueryHandler(ILogger<GetsTransportationByIdsQuery> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<Transportation>?>> Handle(GetsTransportationByIdsQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsTransportationByIds(request.Items, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<Transportation>>(SharedErrors.UnknownError);
        }
    }
}
