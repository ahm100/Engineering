using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetsByProjectOperationIdForVolumes;

public class GetsByProjectOperationIdForVolumesQueryHandler : IQueryHandler<GetsByProjectOperationIdForVolumesQuery, DataResult<List<ProjectOperationDetail>>>
{
    private readonly IProjectOperationDetailRepository _repository;
    private readonly ILogger<GetsByProjectOperationIdForVolumesQueryHandler> _logger;

    public GetsByProjectOperationIdForVolumesQueryHandler(ILogger<GetsByProjectOperationIdForVolumesQueryHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<DataResult<List<ProjectOperationDetail>>?>> Handle(GetsByProjectOperationIdForVolumesQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetsByProjectOperationIdForVolumes(request.ProjectOperationId, 0, 0, ct);

            return result.Data.Any() ?
                new DataResult<List<ProjectOperationDetail>>
                {
                    Data = result.Data,
                    RowCount = result.RowCount
                } : Result.Failure<DataResult<List<ProjectOperationDetail>>>(ProjectOperationDetailErrors.ProjectChildNotFound);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<DataResult<List<ProjectOperationDetail>>>(SharedErrors.UnknownError);
        }
    }
}