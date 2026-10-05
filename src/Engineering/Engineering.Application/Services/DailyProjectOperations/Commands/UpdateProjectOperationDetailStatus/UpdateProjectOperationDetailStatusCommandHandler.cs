using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.DailyProjectOperations.Commands.UpdateProjectOperationStatus;

public class UpdateProjectOperationDetailStatusCommandHandler : ICommandHandler<UpdateProjectOperationDetailStatusCommand, ProjectOperationDetail>
{
    private readonly ILogger<UpdateProjectOperationDetailStatusCommandHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public UpdateProjectOperationDetailStatusCommandHandler(ILogger<UpdateProjectOperationDetailStatusCommandHandler> logger,
                                                            IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(UpdateProjectOperationDetailStatusCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.GetByIdWithDaily(request.ProjectOperationDetailId, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetail?>(SharedErrors.ItemNotFound);

            if (entity.DailyOperations is not null && entity.DailyOperations?.Count == 1)
                entity.SetStatus(Domain.Entities.ProjectOperationDetails.Enums.ProjectOperationDetailStatus.Doing);
            else
                entity.SetStatus(request.Status);

            entity.AddHistory(request.StatusDescription);

            await _repository.Update(entity);
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperationDetail?>(SharedErrors.UnknownError);
        }
    }
}

