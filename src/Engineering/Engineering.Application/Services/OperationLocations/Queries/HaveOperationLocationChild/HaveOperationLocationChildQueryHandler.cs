using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.HaveOperationLocationChild;

public class HaveOperationLocationChildQueryHandler : IQueryHandler<HaveOperationLocationChildQuery, OperationLocation?>
{
    private readonly ILogger<HaveOperationLocationChildQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public HaveOperationLocationChildQueryHandler(ILogger<HaveOperationLocationChildQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(HaveOperationLocationChildQuery request, CT ct)
    {
        try
        {
            var result = await _repository.HaveOperationLocationChild(request.Id, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}