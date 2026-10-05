using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetByCode;

public class GetTransportationByCodeQueryHandler : IQueryHandler<GetTransportationByCodeQuery, Transportation?>
{
    private readonly ILogger<GetTransportationByCodeQueryHandler> _logger;
    private readonly ITransportationRepository _repository;

    public GetTransportationByCodeQueryHandler(ILogger<GetTransportationByCodeQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(GetTransportationByCodeQuery request, CT ct)
    {
        try
        {
            var item = await _repository.FindByCode(request.TransportationCode, request.CompanyId, ct);

            return item ?? Result.Failure<Transportation?>(TransportationErrors.TransportationWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation?>(SharedErrors.UnknownError);
        }
    }
}