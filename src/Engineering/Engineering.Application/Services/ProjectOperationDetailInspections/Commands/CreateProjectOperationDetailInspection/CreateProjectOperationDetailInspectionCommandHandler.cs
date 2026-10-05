using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Commands.CreateProjectOperationDetailInspection;

public class CreateProjectOperationDetailInspectionCommandHandler : ICommandHandler<CreateProjectOperationDetailInspectionCommand, ProjectOperationDetailInspection>
{
    private readonly ILogger<CreateProjectOperationDetailInspectionCommandHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public CreateProjectOperationDetailInspectionCommandHandler(ILogger<CreateProjectOperationDetailInspectionCommandHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailInspection?>> Handle(CreateProjectOperationDetailInspectionCommand request, CT ct)
    {
        try
        {
            var entity = ProjectOperationDetailInspection.Create(request.Project, request.OperationInfo, request.OperationLocation,
                request.ProjectOperation, request.ProjectOperationDetail, request.Length, request.Width, request.Height, request.Weight,
                request.Number, request.InspectionDate, request.Description);

            if (request.Documents != null && request.Documents.Any())
                entity.AddDocuments(request.Documents);

            var result = await _repository.Create(entity, ct);

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailInspection>(SharedErrors.UnknownError);
        }
    }
}
