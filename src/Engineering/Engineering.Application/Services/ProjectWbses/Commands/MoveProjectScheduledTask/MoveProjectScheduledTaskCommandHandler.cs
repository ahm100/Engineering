using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.Contracts.MoveProjectScheduledTask;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Commands.MoveProjectScheduledTask;

public class MoveProjectScheduledTaskCommandHandler : ICommandHandler<MoveProjectScheduledTaskCommand, MoveProjectScheduledTaskResponse>
{
    private readonly ILogger<MoveProjectScheduledTaskCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public MoveProjectScheduledTaskCommandHandler(
        ILogger<MoveProjectScheduledTaskCommandHandler> logger,
        IProjectScheduleTaskRepository repository,
        IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<MoveProjectScheduledTaskResponse?>> Handle(
        MoveProjectScheduledTaskCommand request, CT ct)
    {
        try
        {
            var task = await _repository.GetById(request.TaskId, ct);
            if (task is null)
                return Result.Failure<MoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

            var live = (await _repository.GetByProjectScheduleImportId(task.ProjectScheduleImportId, ct) ?? [])
                .Where(t => !t.IsDeleted).ToList();

            var target = live.FirstOrDefault(t => t.Id == request.TaskId);
            if (target is null)
                return Result.Failure<MoveProjectScheduledTaskResponse>(ProjectErrors.ProjectTaskNotFound)!;

            var siblings = live.Where(t => t.ParentId == target.ParentId)
                               .OrderBy(t => t.SortOrder).ThenBy(t => t.Id).ToList();

            var oldIndex = siblings.FindIndex(t => t.Id == target.Id);
            var newIndex = Math.Clamp(request.NewIndex, 0, siblings.Count - 1);
            if (oldIndex == newIndex)
                return new MoveProjectScheduledTaskResponse(true);

            siblings.RemoveAt(oldIndex);
            siblings.Insert(newIndex, target);

            var changed = new HashSet<ProjectScheduleTask>();
            for (var i = 0; i < siblings.Count; i++)
                if (siblings[i].SortOrder != i + 1)
                {
                    siblings[i].SetSortOrder(i + 1);
                    changed.Add(siblings[i]);
                }

            foreach (var t in TaskOutline.Normalize(live))
                changed.Add(t);

            foreach (var t in changed)
                await _repository.Update(t);

            await _unitOfWork.CommitAsync(ct);
            return new MoveProjectScheduledTaskResponse(true);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<MoveProjectScheduledTaskResponse>(SharedErrors.UnknownError)!;
        }
    }
}
