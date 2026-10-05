using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByName;

public class GetTransportationByNameQueryHandler : IQueryHandler<GetTransportationByNameQuery, Transportation?>
{
    private readonly ILogger<GetTransportationByNameQueryHandler> _logger;
    private readonly ITransportationRepository _repository;

    public GetTransportationByNameQueryHandler(ILogger<GetTransportationByNameQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(GetTransportationByNameQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindByName(request.TransportationName, request.CompanyId, ct);

            return item ?? Result.Failure<Transportation?>(TransportationErrors.TransportationWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation?>(SharedErrors.UnknownError);
        }
    }
}
