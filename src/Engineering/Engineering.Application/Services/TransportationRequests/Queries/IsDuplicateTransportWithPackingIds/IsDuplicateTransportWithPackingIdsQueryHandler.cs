using Engineering.Application.Abstractions.Data.Transportations;

namespace Engineering.Application.Services.TransportationRequests.Queries.IsDuplicateTransportWithPackingIds;

public class IsDuplicateTransportWithPackingIdsQueryHandler : IQueryHandler<IsDuplicateTransportWithPackingIdsQuery, bool?>
{
    private readonly ILogger<IsDuplicateTransportWithPackingIdsQueryHandler> _logger;
    private readonly ITransportationCargoRepository _repository;

    public IsDuplicateTransportWithPackingIdsQueryHandler(ILogger<IsDuplicateTransportWithPackingIdsQueryHandler> logger, ITransportationCargoRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<bool?>> Handle(IsDuplicateTransportWithPackingIdsQuery request, CT ct)
    {
        try
        {
            var item = await _repository.IsDuplicateWithPackingIds(request.PackingIds, ct);

            return item;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<bool?>(SharedErrors.UnknownError);
        }
    }
}