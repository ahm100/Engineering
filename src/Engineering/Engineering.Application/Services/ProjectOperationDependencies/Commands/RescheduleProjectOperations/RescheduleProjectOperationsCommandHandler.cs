using Engineering.Application.Abstractions.Data.ProjectOperations;
using Engineering.Application.Services.ProjectOperationDependencies.Contracts.RescheduleProjectOperations;
using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Application.Services.ProjectOperationDependencies.Commands.RescheduleProjectOperations;

public class RescheduleProjectOperationsCommandHandler : ICommandHandler<RescheduleProjectOperationsCommand, bool>
{
    private readonly ILogger<RescheduleProjectOperationsCommandHandler> _logger;
    private readonly IProjectOperationDependencyRepository _repository;
    private readonly IProjectOperationRepository _poRepo;

    public RescheduleProjectOperationsCommandHandler(ILogger<RescheduleProjectOperationsCommandHandler> logger,
        IProjectOperationDependencyRepository repository,
        IProjectOperationRepository poRepo)
    {
        _logger = logger;
        _repository = repository;
        _poRepo = poRepo;
    }

    public async Task<Result<bool>> Handle(
    RescheduleProjectOperationsCommand request,
    CT ct)
    {
        try
        {
            var ids = await _repository.GetAffectedSuccessorIds(
                request.ProjectOperationId,
                ct);

            if (!ids.Any())
                return true;

            var schedules = await _poRepo.GetOperationsForReschedule(
                ids,
                ct);

            var ordered = TopologicalSort(schedules);

            foreach (var operation in ordered)
            {
                CalculateOperationDate(operation);
            }

            var entities = await _poRepo.GetByIds(
                ids,
                ct);

            foreach (var entity in entities)
            {
                var schedule = ordered.First(x => x.Id == entity.Id);

                entity.SetPlannedStartDate(schedule.PlannedStartDate);
                entity.SetPlannedFinishDate(schedule.PlannedFinishDate);
                entity.SetPlannedDuration(schedule.PlannedDuration);
                await _poRepo.Update(entity);
            }

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);
            return Result.Failure<bool>(
                SharedErrors.UnknownError);
        }
    }

    private List<ProjectOperationScheduleModel> TopologicalSort(
    List<ProjectOperationScheduleModel> operations)
    {
        var result = new List<ProjectOperationScheduleModel>();

        var operationIds = operations
            .Select(x => x.Id)
            .ToHashSet();

        var dependencies = operations.ToDictionary(
            x => x.Id,
            x => x.Predecessors.Count(p =>
                operationIds.Contains(p.Id)));

        var successors = operations
            .SelectMany(x => x.Predecessors.Select(p => new
            {
                PredecessorId = p.Id,
                SuccessorId = x.Id
            }))
            .GroupBy(x => x.PredecessorId)
            .ToDictionary(
                x => x.Key,
                x => x.Select(y => y.SuccessorId).ToList());

        var queue = new Queue<ProjectOperationScheduleModel>(
            operations.Where(x =>
                dependencies[x.Id] == 0));

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            result.Add(current);

            if (!successors.TryGetValue(current.Id, out var successorIds))
                continue;

            foreach (var successorId in successorIds)
            {
                dependencies[successorId]--;


                if (dependencies[successorId] == 0)
                {
                    queue.Enqueue(
                        operations.First(x =>
                            x.Id == successorId));
                }
            }
        }

        return result;
    }

    private void CalculateOperationDate(ProjectOperationScheduleModel operation)
    {
        DateTime? calculatedStart = null;
        DateTime? calculatedFinish = null;

        foreach (var dependency in operation.Predecessors)
        {
            if (!dependency.PlannedStartDate.HasValue ||
                !dependency.PlannedFinishDate.HasValue)
                continue;


            switch (dependency.DependencyType)
            {
                case ProjectOperationDependencyType.FS:

                    calculatedStart = Max(
                        calculatedStart,
                        dependency.PlannedFinishDate.Value
                            .AddDays(dependency.LagDays));

                    break;


                case ProjectOperationDependencyType.SS:

                    calculatedStart = Max(
                        calculatedStart,
                        dependency.PlannedStartDate.Value
                            .AddDays(dependency.LagDays));

                    break;


                case ProjectOperationDependencyType.FF:

                    calculatedFinish = Max(
                        calculatedFinish,
                        dependency.PlannedFinishDate.Value
                            .AddDays(dependency.LagDays));

                    break;


                case ProjectOperationDependencyType.SF:

                    calculatedFinish = Max(
                        calculatedFinish,
                        dependency.PlannedStartDate.Value
                            .AddDays(dependency.LagDays));

                    break;
            }
        }

        if (calculatedStart.HasValue)
        {
            operation.PlannedStartDate = calculatedStart;

            if (operation.PlannedDuration.HasValue)
            {
                operation.PlannedFinishDate =
                    calculatedStart.Value.AddDays(
                        operation.PlannedDuration.Value - 1);
            }
        }

        if (calculatedFinish.HasValue)
        {
            operation.PlannedFinishDate = calculatedFinish;

            if (operation.PlannedDuration.HasValue)
            {
                operation.PlannedStartDate =
                    calculatedFinish.Value.AddDays(
                        -(operation.PlannedDuration.Value - 1));
            }
        }
    }

    private DateTime Max(DateTime? current, DateTime value)
    {
        if (!current.HasValue)
            return value;

        return current.Value > value
            ? current.Value
            : value;
    }
}