using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.RemoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.WBS;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PatternContexts;

namespace Engineering.Application.Services.ProjectWbses.Commands.RemoveProjectScheduledTask;

public class RemoveProjectScheduledTaskCommandHandler : ICommandHandler<RemoveProjectScheduledTaskCommand, RemoveProjectScheduledTaskResponse?>
{
    private readonly ILogger<RemoveProjectScheduledTaskCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;
    private readonly IProjectScheduleTaskDependencyRepository _dependencyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectScheduleTaskOperationRepository _operationRepository;

    public RemoveProjectScheduledTaskCommandHandler(
        ILogger<RemoveProjectScheduledTaskCommandHandler> logger,
        IProjectScheduleTaskRepository repository,
        IUnitOfWork unitOfWork,
        IProjectScheduleTaskDependencyRepository dependencyRepository,
        IProjectScheduleTaskOperationRepository operationRepository)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dependencyRepository = dependencyRepository;
        _operationRepository = operationRepository;
    }

    public async Task<Result<RemoveProjectScheduledTaskResponse?>> Handle(
        RemoveProjectScheduledTaskCommand request, CT ct)
    {
        try
        {
            var task = await _repository.GetById(request.TaskId, ct);
            if (task is null)
                return Result.Failure<RemoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

            var all = (await _repository.GetByProjectScheduleImportId(task.ProjectScheduleImportId, ct) ?? [])
                .Where(t => !t.IsDeleted).ToList();

            var target = all.FirstOrDefault(t => t.Id == request.TaskId);
            if (target is null)
                return Result.Failure<RemoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

            // the task and its whole subtree
            var byParent = all.ToLookup(t => t.ParentId);
            var subtree = new List<ProjectScheduleTask>();
            void Collect(ProjectScheduleTask t)
            {
                subtree.Add(t);
                foreach (var c in byParent[t.Id]) Collect(c);
            }
            Collect(target);
            var ids = subtree.Select(t => t.Id).ToHashSet();

            foreach (var t in subtree)
            {
                t.SoftDelete();
                await _repository.Update(t);
            }

            // dependencies touching any deleted task
            var deps = await _dependencyRepository.GetByTaskIds(ids.ToList(), ct) ?? [];
            foreach (var d in deps.Where(d => ids.Contains(d.PredecessorTaskId) || ids.Contains(d.SuccessorTaskId)))
            {
                d.SoftDelete();
                await _dependencyRepository.Update(d);
            }

            var ops = await _operationRepository.GetByTaskIds(ids.ToList(), ct) ?? [];
            foreach (var d in ops.Where(d => ids.Contains(d.ProjectScheduleTaskId)))
            {
                d.SoftDelete();
                await _operationRepository.Update(d);
            }

            var remaining = all.Where(t => !ids.Contains(t.Id)).ToList();
            foreach (var t in TaskOutline.Normalize(remaining))
                await _repository.Update(t);

            await _unitOfWork.CommitAsync(ct);
            return new RemoveProjectScheduledTaskResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<RemoveProjectScheduledTaskResponse>(SharedErrors.UnknownError)!;
        }
    }
}
