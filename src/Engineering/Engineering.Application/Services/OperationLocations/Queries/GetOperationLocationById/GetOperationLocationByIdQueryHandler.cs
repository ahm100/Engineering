using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationById;

public class GetOperationLocationByIdQueryHandler : IQueryHandler<GetOperationLocationByIdQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationByIdQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationByIdQueryHandler(ILogger<GetOperationLocationByIdQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByIdWithCostCenter(request.Id, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}