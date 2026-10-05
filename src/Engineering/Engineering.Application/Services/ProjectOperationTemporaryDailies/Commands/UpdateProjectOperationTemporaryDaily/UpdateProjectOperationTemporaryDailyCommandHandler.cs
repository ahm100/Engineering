using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Application.Services.ProjectOperationTemporaryDailies.Commands.UpdateProjectOperationTemporaryDaily;

public class UpdateProjectOperationTemporaryDailyCommandHandler : ICommandHandler<UpdateProjectOperationTemporaryDailyCommand, ProjectOperationTemporaryDaily>
{
    private readonly ILogger<UpdateProjectOperationTemporaryDailyCommandHandler> _logger;
    private readonly IProjectOperationTemporaryDailyRepository _repository;

    public UpdateProjectOperationTemporaryDailyCommandHandler(ILogger<UpdateProjectOperationTemporaryDailyCommandHandler> logger, IProjectOperationTemporaryDailyRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationTemporaryDaily?>> Handle(UpdateProjectOperationTemporaryDailyCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationTemporaryDaily>(ProjectOperationTemporaryDailyErrors.TemporaryDailyDocumentWithIdNotFound);

            entity.SetStartDate(request.StartDate);
            entity.SetDescription(request.Description);
            entity.SetEndDate(request.EndDate);
            entity.ChangeStatus(request.Status);
            entity.SetProjectOperation(request.ProjectOperation);
            entity.SetProject(request.Project);
            entity.SetCostCenter(request.CostCenter);
            entity.AddDocuments(request.Urls);

            await _repository.Update(entity);

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationTemporaryDaily>(SharedErrors.UnknownError);
        }
    }
}
