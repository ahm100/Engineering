using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperations.Commands.CreateEContractPOperation;

public class CreateEContractPOperationCommandHandler : ICommandHandler<
    CreateEContractPOperationCommand, List<ProjectOperation>>
{
    private readonly ILogger<CreateEContractPOperationCommand> _logger;
    private readonly IProjectOperationRepository _repository;

    public CreateEContractPOperationCommandHandler(
        ILogger<CreateEContractPOperationCommand> logger,
        IProjectOperationRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<Result<List<ProjectOperation>?>> Handle(
        CreateEContractPOperationCommand request, CT ct)
    {
        try
        {
            var entities = new List<ProjectOperation>();
            foreach (var operation in request.CreatePOperations!)
            {
                var considerations = new List<EmployerConsideration>();
                if (operation.FlagIds.HasAny())
                    if (request.FlagedConsiderations.HasAny())
                        considerations = request.FlagedConsiderations?
                            .Where(x => operation.FlagIds!.Contains(x.FlagId!)).ToList();

                var operationInfo = request.OperationInfos
                    .FirstOrDefault(x => x.Id == operation.OperationInfoId);

                int? baselineDuration =
                    operation.BaselineStartDate.HasValue && operation.BaselineFinishDate.HasValue
                    ? (operation.BaselineFinishDate.Value - operation.BaselineStartDate.Value).Days
                    : null;
                var entity = await _repository.Create(new ProjectOperation(
                    request.EmployerContract.Project,
                    operationInfo!,
                    request.EmployerContract,
                    operation.Workload,
                    0,
                    null,
                    operation.Priority,
                    operationInfo!.UnitOfMeasurementId,
                    ProjectOperationStatus.NotStarted,
                    operation.GoodsInProgress,
                    operation.BaselineStartDate,
                    operation.BaselineFinishDate,
                    baselineDuration,
                    operation.Description,
                    operation.Urls,
                    considerations,
                    request.EmployerContract.EmployerContractHead.CompanyId), ct);

                entities.Add(entity);
            }

            return entities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<List<ProjectOperation>>(SharedErrors.UnknownError);
        }
    }
}