using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetDependencyByPOId;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetDependencyByPOId;

public class GetDependencyByPOIdQueryHandler : IQueryHandler<GetDependencyByPOIdQuery, GetDependencyByPOIdResponse?>
{
    private readonly ILogger<GetDependencyByPOIdQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public GetDependencyByPOIdQueryHandler(ILogger<GetDependencyByPOIdQueryHandler> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetDependencyByPOIdResponse?>> Handle(GetDependencyByPOIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetDependencyByPOId(request.ProjectOperationId,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new GetDependencyByPOIdResponse(result.Data, result.RowCount) :
                Result.Failure<GetDependencyByPOIdResponse?>(ProjectOperationDetailErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetDependencyByPOIdResponse?>(SharedErrors.UnknownError);
        }
    }
}