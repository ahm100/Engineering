using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.UnAssignContract;

public class UnAssignContractCommandHandler : ICommandHandler<UnAssignContractCommand, ProjectOperation>
{
    private readonly ILogger<UnAssignContractCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public UnAssignContractCommandHandler(ILogger<UnAssignContractCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(UnAssignContractCommand request, CT ct)
    {
        try
        {
            var entity = await _repository.FindForDelete(request.Id, ct);
            if (entity is null)
                return Result.Failure<ProjectOperation>(ProjectOperationErrors.ProjectOperationWithIdNotFound);

            ///TODO Employers
            //entity.UnSetEmployerContract();

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