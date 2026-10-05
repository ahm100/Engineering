using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.GetFltrDependency;

namespace Engineering.Application.Services.ProjectOperationDependencies.Queries.GetFltrDependency;

public class GetFltrDependencyQueryHandler : IQueryHandler<GetFltrDependencyQuery, GetFltrDependencyResponse?>
{
    private readonly ILogger<GetFltrDependencyQueryHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;

    public GetFltrDependencyQueryHandler(ILogger<GetFltrDependencyQueryHandler> logger, IProjectOperationDependencyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<GetFltrDependencyResponse?>> Handle(GetFltrDependencyQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetFltrDependency(request.ProjectOperationIds,
                request.FilterData,
                request.PageIndex,
                request.PageSize, ct);

            return result.Data.Any() ?
                new GetFltrDependencyResponse(result.Data, result.RowCount) :
                Result.Failure<GetFltrDependencyResponse?>(ProjectOperationDetailErrors.DependencyWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<GetFltrDependencyResponse?>(SharedErrors.UnknownError);
        }
    }
}
