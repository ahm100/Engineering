using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdLessIncludes;

public class GetProjectOperationDetailByIdLessIncludesQueryHandler : IQueryHandler<GetProjectOperationDetailByIdLessIncludesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailByIdLessIncludesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailByIdLessIncludesQueryHandler(ILogger<GetProjectOperationDetailByIdLessIncludesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailByIdLessIncludesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailByIdLessIncludes(
                request.Id,
                ct);

            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
