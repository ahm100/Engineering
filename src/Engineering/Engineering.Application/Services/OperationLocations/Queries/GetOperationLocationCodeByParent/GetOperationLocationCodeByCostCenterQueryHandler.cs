using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationCodeByParent;

public class GetOperationLocationCodeByParentQueryHandler : IQueryHandler<GetOperationLocationCodeByParentQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationCodeByParentQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationCodeByParentQueryHandler(ILogger<GetOperationLocationCodeByParentQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationCodeByParentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocationCodeByParent(request.PrivateCode, request.ParentId, request.CompanyId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithCodeNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}
