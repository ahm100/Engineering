using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.UpdateProjectOperationDetailDate;

public class UpdateProjectOperationDetailDateCommandHandler : ICommandHandler<UpdateProjectOperationDetailDateCommand, ProjectOperationDetail>
{
    private readonly ILogger<UpdateProjectOperationDetailDateCommandHandler> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public UpdateProjectOperationDetailDateCommandHandler(ILogger<UpdateProjectOperationDetailDateCommandHandler> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(UpdateProjectOperationDetailDateCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.ProjectOperationDetailId, ct);

            if (entity is null)
                return Result.Failure<ProjectOperationDetail?>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            entity.SetStartDate(request.StartDate);
            entity.SetEndDate(request.EndDate);

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
