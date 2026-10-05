using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailWithMachineryVolumes;

public class GetProjectOperationDetailWithMachineryVolumesQueryHandler : IQueryHandler<GetProjectOperationDetailWithMachineryVolumesQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailWithMachineryVolumesQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailWithMachineryVolumesQueryHandler(ILogger<GetProjectOperationDetailWithMachineryVolumesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailWithMachineryVolumesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailWithMachineryVolumes(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
