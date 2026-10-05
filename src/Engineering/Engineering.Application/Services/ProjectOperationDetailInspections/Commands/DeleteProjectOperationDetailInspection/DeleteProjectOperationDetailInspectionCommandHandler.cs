using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetailInspections.Commands.DeleteProjectOperationDetailInspection;

public class DeleteProjectOperationDetailInspectionCommandHandler : ICommandHandler<DeleteProjectOperationDetailInspectionCommand, ProjectOperationDetailInspection>
{
    private readonly ILogger<DeleteProjectOperationDetailInspectionCommandHandler> _logger;
    private readonly IProjectOperationDetailInspectionRepository _repository;

    public DeleteProjectOperationDetailInspectionCommandHandler(ILogger<DeleteProjectOperationDetailInspectionCommandHandler> logger, IProjectOperationDetailInspectionRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetailInspection?>> Handle(DeleteProjectOperationDetailInspectionCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.ProjectOperationDetailInspectionId, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetailInspection>(ProjectOperationDetailInspectionErrors.InspectionWithIdNotFound);
            if (entity.IsDeleted)
                return Result.Failure<ProjectOperationDetailInspection>(ProjectOperationDetailInspectionErrors.IsDeleted);

            entity.SetIsDeleted();

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
