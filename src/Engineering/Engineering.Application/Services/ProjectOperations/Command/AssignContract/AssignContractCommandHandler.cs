using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.AssignContract;

public class AssignContractCommandHandler : ICommandHandler<AssignContractCommand, ProjectOperation>
{
    private readonly ILogger<AssignContractCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public AssignContractCommandHandler(ILogger<AssignContractCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(AssignContractCommand request, CT ct)
    {
        try
        {
            var entity = request.ProjectOperation;

            ///TODO Employers
            //entity.SetEmployerContract(request.EmployerContract);

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