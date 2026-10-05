using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetProjectOperationForRequestGoodsSupply;

public class GetProjectOperationForRequestGoodsSupplyQueryHandler : IQueryHandler<GetProjectOperationForRequestGoodsSupplyQuery, ProjectOperation>
{
    private readonly ILogger<GetProjectOperationForRequestGoodsSupplyQueryHandler> _logger;
    private readonly IProjectOperationRepository _repository;

    public GetProjectOperationForRequestGoodsSupplyQueryHandler(ILogger<GetProjectOperationForRequestGoodsSupplyQueryHandler> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(GetProjectOperationForRequestGoodsSupplyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationForRequestGoodsSupply(request.Id, ct);
            return result ?? Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}
