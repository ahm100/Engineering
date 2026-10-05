using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailByIdWithoutInclude;

public class GetProjectOperationDetailByIdWithoutIncludeQueryHandler : IQueryHandler<GetProjectOperationDetailByIdWithoutIncludeQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailByIdWithoutIncludeQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailByIdWithoutIncludeQueryHandler(ILogger<GetProjectOperationDetailByIdWithoutIncludeQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailByIdWithoutIncludeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailByIdWithoutInclude(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
