using Engineering.Application.Abstractions.Data.ProjectOperations;
using ProjectOperation = Engineering.Domain.Entities.ProjectOperations.ProjectOperation;

namespace Engineering.Application.Services.ProjectOperations.Commands.Create;

public class CreateProjectOperationCommandHandler : ICommandHandler<CreateProjectOperationCommand, ProjectOperation>
{
    private readonly ILogger<CreateProjectOperationCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public CreateProjectOperationCommandHandler(ILogger<CreateProjectOperationCommand> logger, IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<ProjectOperation?>> Handle(CreateProjectOperationCommand request, CT ct)
    {
        try
        {
            int? baselineDuration = null;

            if (request.BaselineStartDate.HasValue && request.BaselineFinishDate.HasValue && !request.BaselineDuration.HasValue)
            {
                baselineDuration = (request.BaselineFinishDate.Value - request.BaselineStartDate.Value).Days;
            }

            var entity = ProjectOperation.Create(
                request.Project,
                request.OperationInfo,
                request.EmployerContract,
                request.Workload,
                request.TolerancePercentage,
                request.Price,
                request.Priority,
                request.UnitOfMeasurementId,
                request.ProjectOperationStatus,
                request.GoodsInProgress,
                request.BaselineStartDate,
                request.BaselineFinishDate,
                request.BaselineDuration ?? baselineDuration,
                request.Description,
                request.Urls,
                null,
                request.CompanyId);

            return await _repository.Create(entity, ct);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectOperation>(SharedErrors.UnknownError);
        }
    }
}