using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByName;

public class GetOperationLocationByNameQueryHandler : IQueryHandler<GetOperationLocationByNameQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationByNameQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationByNameQueryHandler(ILogger<GetOperationLocationByNameQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationByNameQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByName(request.PrivateName, request.CompanyId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}