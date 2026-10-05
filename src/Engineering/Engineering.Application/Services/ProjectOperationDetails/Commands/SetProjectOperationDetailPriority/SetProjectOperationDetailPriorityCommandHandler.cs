using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using ProjectOperationDetail = Engineering.Domain.Entities.ProjectOperationDetails.ProjectOperationDetail;

namespace Engineering.Application.Services.ProjectOperationDetails.Commands.SetProjectOperationDetailPriority;

public class SetProjectOperationDetailPriorityCommandHandler : ICommandHandler<SetProjectOperationDetailPriorityCommand, ProjectOperationDetail>
{
    private readonly ILogger<SetProjectOperationDetailPriorityCommand> _logger;
    private readonly IProjectOperationDetailRepository _repository;

    public SetProjectOperationDetailPriorityCommandHandler(ILogger<SetProjectOperationDetailPriorityCommand> logger, IProjectOperationDetailRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperationDetail?>> Handle(SetProjectOperationDetailPriorityCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindById(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperationDetail>(ProjectOperationDetailErrors.ProjectOperationDetailWithIdNotFound);

            entity.SetPriority(request.Priority);
            await _repository.Update(entity);

            return entity;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<ProjectOperationDetail>(SharedErrors.UnknownError);
        }
    }
}