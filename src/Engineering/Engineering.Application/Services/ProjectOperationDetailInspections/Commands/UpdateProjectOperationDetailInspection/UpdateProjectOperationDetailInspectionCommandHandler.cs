using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Commands.UpdateProjectOperationDetailInspection;

public class UpdateProjectOperationDetailInspectionCommandHandler : ICommandHandler<UpdateProjectOperationDetailInspectionCommand, ProjectOperationDetailInspection>
{
    private readonly ILogger<UpdateProjectOperationDetailInspectionCommandHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public UpdateProjectOperationDetailInspectionCommandHandler(ILogger<UpdateProjectOperationDetailInspectionCommandHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailInspection?>> Handle(UpdateProjectOperationDetailInspectionCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailInspection>(ProjectOperationDetailInspectionErrors.InspectionWithIdNotFound);

            entity.SetLength(request.Length);
            entity.SetWidth(request.Width);
            entity.SetHeight(request.Height);
            entity.SetWeight(request.Weight);
            entity.SetNumber(request.Number);
            entity.SetDescription(request.Description);
            entity.SetOperationInfo(request.OperationInfo);
            entity.SetOperationLocation(request.OperationLocation);
            entity.SetProjectOperation(request.ProjectOperation);
            entity.SetProject(request.Project);
            entity.SetProjectOperationDetail(request.ProjectOperationDetail);
            entity.SetInspectionDate(request.InspectionDate);
            entity.AddDocuments(request.Documents);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetailInspection>(SharedErrors.UnknownError);
        }
    }
}
