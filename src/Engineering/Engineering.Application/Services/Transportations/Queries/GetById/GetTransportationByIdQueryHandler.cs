using Engineering.Application.Abstractions.Data.Transportations;
using Transportation = Engineering.Domain.Entities.Transportations.Transportation;

namespace Engineering.Application.Services.Transportations.Queries.GetById;

public class GetTransportationByIdQueryHandler : IQueryHandler<GetTransportationByIdQuery, Transportation?>
{
    private readonly ILogger<GetTransportationByIdQueryHandler> _logger;
    private readonly ITransportationRepository _repository;

    public GetTransportationByIdQueryHandler(ILogger<GetTransportationByIdQueryHandler> logger, ITransportationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<Transportation?>> Handle(GetTransportationByIdQuery request, CT ct)
    {
        try
        {
            var item = await _repository.GetById(request.Id, ct);

            return item ?? Result.Failure<Transportation?>(TransportationErrors.TransportationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<Transportation?>(SharedErrors.UnknownError);
        }
    }
}