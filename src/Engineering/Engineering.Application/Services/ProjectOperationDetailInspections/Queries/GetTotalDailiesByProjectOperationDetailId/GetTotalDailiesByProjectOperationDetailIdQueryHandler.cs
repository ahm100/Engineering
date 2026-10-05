using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetTotalDailiesByProjectOperationDetailId;

public class GetTotalDailiesByProjectOperationDetailIdQueryHandler : IQueryHandler<GetTotalDailiesByProjectOperationDetailIdQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetTotalDailiesByProjectOperationDetailIdQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetTotalDailiesByProjectOperationDetailIdQueryHandler(ILogger<GetTotalDailiesByProjectOperationDetailIdQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetTotalDailiesByProjectOperationDetailIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailByIdForTotal(request.ProjectOperationDetailId, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
