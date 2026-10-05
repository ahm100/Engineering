using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithVolumes;

public class GetProjectOperationDetailWithVolumesQueryHandler : IQueryHandler<GetProjectOperationDetailWithVolumesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailWithVolumesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailWithVolumesQueryHandler(ILogger<GetProjectOperationDetailWithVolumesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailWithVolumesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailWithVolumes(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
