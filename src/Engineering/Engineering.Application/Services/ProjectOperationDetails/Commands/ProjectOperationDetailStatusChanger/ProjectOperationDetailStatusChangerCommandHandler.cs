using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.ProjectOperationDetailStatusChanger;

public class ProjectOperationDetailStatusChangerCommandHandler : ICommandHandler<ProjectOperationDetailStatusChangerCommand, ProjectOperationDetail>
{
    private readonly ILogger<ProjectOperationDetailStatusChangerCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public ProjectOperationDetailStatusChangerCommandHandler(ILogger<ProjectOperationDetailStatusChangerCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(ProjectOperationDetailStatusChangerCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByProjectOperationDetailId(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            //if (entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.DailyOperations.Count <= 0 &&
            //    (request.Status != ProjectOperationDetailStatus.NotStarted && request.Status != ProjectOperationDetailStatus.Doing))
            //{
            //    return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.NoHaveDaily);
            //}

            //if (entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.DailyOperations.Count > 0 && request.Status == ProjectOperationDetailStatus.NotStarted)
            //    return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.CanNotToNotStarted);

            //if (entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.DailyOperations.Count <= 0)
            //{
            //    if (request.Status == ProjectOperationDetailStatus.NotStarted || request.Status == ProjectOperationDetailStatus.Doing)
            //        entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.SetStatus(request.Status);
            //}
            //else
            //{
            //    entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.SetStatus(request.Status);
            //    if (entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()!.DailyOperations.Count > 0)
            //        entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.AddDailyProjectOperation();
            //}

            entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.SetStatus(request.Status);
            //if (entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()!.DailyOperations.Count > 0)
            //    entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()?.AddDailyProjectOperation();

            if (entity.ProjectOperationDetails.All(x => x.Status == request.Status))
                entity.SetProjectOperationStatus((ProjectOperationStatus)request.Status);
            else
                entity.SetProjectOperationStatus(ProjectOperationStatus.Doing);

            entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault()!.AddHistory(request.StatusDescription);

            await _repository.Update(entity);

            return entity.ProjectOperationDetails.Where(x => x.Id == request.Id).FirstOrDefault();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}
