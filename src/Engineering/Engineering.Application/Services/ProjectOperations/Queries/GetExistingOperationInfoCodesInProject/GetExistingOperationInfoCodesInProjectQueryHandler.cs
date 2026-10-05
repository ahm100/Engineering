using Engineering.Application.Abstractions.Data.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations.Queries.GetExistingOperationInfoCodesInProject;

public class GetExistingOperationInfoCodesInProjectQueryHandler : IQueryHandler<GetExistingOperationInfoCodesInProjectQuery, List<string>>
{
    private readonly IProjectOperationRepository _repository;
    private readonly ILogger<GetExistingOperationInfoCodesInProjectQueryHandler> _logger;

    public GetExistingOperationInfoCodesInProjectQueryHandler(
        ILogger<GetExistingOperationInfoCodesInProjectQueryHandler> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<string>?>> Handle(GetExistingOperationInfoCodesInProjectQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetExistingOperationInfoCodesInProject(
                request.ProjectId,
                request.OperationInfoCodes,
                ct);

            return Result.Success<List<string>?>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<string>?>(SharedErrors.UnknownError);
        }
    }
}