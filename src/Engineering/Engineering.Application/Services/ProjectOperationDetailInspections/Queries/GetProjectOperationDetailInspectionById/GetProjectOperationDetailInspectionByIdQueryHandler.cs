using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetProjectOperationDetailInspectionById;

public class GetProjectOperationDetailInspectionByIdQueryHandler : IQueryHandler<GetProjectOperationDetailInspectionByIdQuery, ProjectOperationDetailInspection>
{
    private readonly ILogger<GetProjectOperationDetailInspectionByIdQueryHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public GetProjectOperationDetailInspectionByIdQueryHandler(ILogger<GetProjectOperationDetailInspectionByIdQueryHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailInspection?>> Handle(GetProjectOperationDetailInspectionByIdQuery request, CT ct)
    {
        try
        {
            var result = await _repository.GetById(request.ProjectOperationDetailInspectionId, ct);
            return result ?? Result.Failure<ProjectOperationDetailInspection>(ProjectOperationDetailInspectionErrors.InspectionWithIdNotFound);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetailInspection>(SharedErrors.UnknownError);
        }
    }
}
