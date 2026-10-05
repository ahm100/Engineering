using Engineering.Application.Abstractions.Data.Transportations;
using Engineering.Application.Services.Transportations.Queries.GetById;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByType;

public class GetTransportationByTypeQueryHandler : IQueryHandler<GetTransportationByTypeQuery, Transportation?>
{
    private readonly ILogger<GetTransportationByIdQueryHandler> _logger;
    private readonly ITransportationRepository _repository;

    public GetTransportationByTypeQueryHandler(ILogger<GetTransportationByIdQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(GetTransportationByTypeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetByType(request.TransportationType, ct);

            return item ?? Result.Failure<Transportation?>(TransportationErrors.TransportationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation?>(SharedErrors.UnknownError);
        }
    }
}