using Engineering.Application.Services.Projects.Queries.GetProjectByIdIncludeless;
using Engineering.Domain.Entities.OperationInfos;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperations;

partial class ProjectOperationLogic
{

    private async Task<Result<List<ProjectOperationAction>?>> CreateProjectOperationActionsCommand(
     List<OperationInfoAction> actions, ProjectOperation pOperation, CT ct)
    {
        try
        {
            List<ProjectOperationAction> entities = [];
            foreach (var item in actions)
            {
                var entity = await _projectOperationActionRepository.Create(new ProjectOperationAction(
                pOperation,
                item), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationAction>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<List<ProjectOperationAction>?>> DeleteProjectOperationActionsHandler(
         List<long> ids, CT ct)
    {
        try
        {
            var entities = await _projectOperationActionRepository.GetPOActionsByIds(ids, ct);
            if (entities is null)
                return Result.Failure<List<ProjectOperationAction>>(OperationInfoErrors.OperationInfoActionWithIdNotFound);

            foreach (var entity in entities)
            {
                if (entity.IsDeleted)
                    return Result.Failure<List<ProjectOperationAction>>(OperationInfoErrors.OperationInfoActionIsDeleted);

                entity.SoftDelete();
                await _projectOperationActionRepository.Update(entity);
            }
            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperationAction>>(SharedErrors.UnknownError);
        }
    }

    private async Task<Result<ProjectOperation?>> ChangeProject(
         long id, long projectId, CT ct)
    {
        try
        {
            var project = await _mediator.Send(new GetProjectByIdIncludelessQuery(projectId), ct);
            var entity = await _repository.GetProjectOperationById(id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            entity.SetProject(project.Value);
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }

}
