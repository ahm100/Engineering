using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetProjectOperationDetailDoneVolume;

public class GetProjectOperationDetailDoneVolumeQueryHandler : IQueryHandler<GetProjectOperationDetailDoneVolumeQuery, ProjectOperationDetail>
{
    private readonly ILogger<GetProjectOperationDetailDoneVolumeQueryHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public GetProjectOperationDetailDoneVolumeQueryHandler(ILogger<GetProjectOperationDetailDoneVolumeQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(GetProjectOperationDetailDoneVolumeQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetProjectOperationDetailDoneVolume(request.Id, ct);
            return result ?? Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
