using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ConsumableVolumes.Queries.GetProjectOperationDetailVolumes;

public class GetProjectOperationDetailVolumesQueryHandler : IQueryHandler<GetProjectOperationDetailVolumesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailVolumesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailVolumesQueryHandler(ILogger<GetProjectOperationDetailVolumesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailVolumesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailVolumes(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
