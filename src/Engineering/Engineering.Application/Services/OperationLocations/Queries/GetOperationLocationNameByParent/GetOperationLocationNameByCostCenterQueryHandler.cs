using Engineering.Application.Abstractions.Data.OperationLocations;
using OperationLocation = Engineering.Domain.Entities.OperationLocations.OperationLocation;

namespace Engineering.Application.Services.OperationLocations.Queries.GetOperationLocationNameByParent;

public class GetOperationLocationNameByParentQueryHandler : IQueryHandler<GetOperationLocationNameByParentQuery, OperationLocation?>
{
    private readonly ILogger<GetOperationLocationNameByParentQueryHandler> _logger;
    private readonly IOperationLocationRepository _repository;

    public GetOperationLocationNameByParentQueryHandler(ILogger<GetOperationLocationNameByParentQueryHandler> logger, IOperationLocationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<OperationLocation?>> Handle(GetOperationLocationNameByParentQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetOperationLocationNameByParent(request.PrivateName, request.ParentId, request.CompanyId, ct);

            return result ?? Result.Failure<OperationLocation?>(OperationLocationErrors.OperationLocationWithNameNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<OperationLocation?>(SharedErrors.UnknownError);
        }
    }
}
