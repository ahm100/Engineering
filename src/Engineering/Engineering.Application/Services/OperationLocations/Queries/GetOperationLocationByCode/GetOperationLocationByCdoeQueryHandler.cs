using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationByCode;

public class GetOperationLocationByCodeQueryHandler : IQueryHandler<GetOperationLocationByCodeQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationByCodeQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationByCodeQueryHandler(ILogger<GetOperationLocationByCodeQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationByCodeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.FindByCode(request.PrivateCode, request.CompanyId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}