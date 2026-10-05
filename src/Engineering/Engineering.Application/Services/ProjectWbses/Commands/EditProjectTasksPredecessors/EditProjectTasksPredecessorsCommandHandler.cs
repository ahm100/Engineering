using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Engineering.Application.Services.ProjectWbses.Commands.EditProjectTasksPredecessors;

public class EditProjectTasksPredecessorsCommandHandler : ICommandHandler<EditProjectTasksPredecessorsCommand, ProjectScheduleTask?>
{
    private static readonly Regex PredecessorTokenRegex = new(
        @"^\s*(?<row>\d+)\s*(?<type>FS|SS|FF|SF)?\s*(?<lag>[+-]\s*\d+(\.\d+)?\s*d?)?\s*$",
        RegexOptions.IgnoreCase | RegexOptions.Compiled);

    private readonly ILogger<EditProjectTasksPredecessorsCommandHandler> _logger;
    private readonly IProjectScheduleTaskRepository _taskRepository;
    private readonly IProjectScheduleTaskDependencyRepository _dependencyRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectCalendarRepository _calendarRepository;

    public EditProjectTasksPredecessorsCommandHandler(
        ILogger<EditProjectTasksPredecessorsCommandHandler> logger,
        IProjectScheduleTaskRepository taskRepository,
        IProjectScheduleTaskDependencyRepository dependencyRepository,
        IProjectScheduleImportRepository importRepository,
        IProjectCalendarRepository calendarRepository)
    {
        _logger = logger;
        _taskRepository = taskRepository;
        _dependencyRepository = dependencyRepository;
        _importRepository = importRepository;
        _calendarRepository = calendarRepository;
    }

    private record ParsedPredecessor(ProjectScheduleTask Task, ProjectScheduleDependencyType Type, long LagMinutes);

    public async Task<Result<ProjectScheduleTask?>> Handle(EditProjectTasksPredecessorsCommand request, CT ct)
    {
        try
        {
            var task = await _taskRepository.GetById(request.Id, ct);
            if (task is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

            var all = (await _taskRepository.GetByProjectScheduleImportId(task.ProjectScheduleImportId, ct) ?? [])
                .Where(t => !t.IsDeleted).ToList();

            // typed number = row in the depth-first list, the same number the grid shows
            var rowToTask = TaskOutline.Flatten(all)
                .Select((t, i) => (Row: i + 1, Task: t))
                .ToDictionary(x => x.Row, x => x.Task);
            var byId = all.ToDictionary(t => t.Id);

            var import = await _importRepository.GetById(task.ProjectScheduleImportId, ct);
            if (import is null)
                return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectNotFound)!;
            var cals = await _calendarRepository.GetByProjectId(import.ProjectId, ct);
            var minutesPerDay = (cals?.FirstOrDefault(c => c.IsDefault) ?? cals?.FirstOrDefault())?.MinutesPerDay ?? 480;

            bool IsAncestor(ProjectScheduleTask a, ProjectScheduleTask b)
            {
                for (var p = b.ParentId; p is not null; p = byId.GetValueOrDefault(p.Value)?.ParentId)
                    if (p == a.Id) return true;
                return false;
            }

            var rawTokens = NormalizeDigits(request.Predecessors ?? string.Empty)
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var parsed = new List<ParsedPredecessor>();
            var seen = new HashSet<long>();

            foreach (var rawToken in rawTokens)
            {
                var match = PredecessorTokenRegex.Match(rawToken);
                if (!match.Success || !int.TryParse(match.Groups["row"].Value, out var row))
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.InvalidPredecessorFormat)!;

                if (!rowToTask.TryGetValue(row, out var pred))
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.ProjectTaskNotFound)!;

                if (pred.Id == task.Id || !seen.Add(pred.Id))
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.DuplicatePredecessor)!;

                if (IsAncestor(pred, task) || IsAncestor(task, pred))
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.PredecessorWithAncestorOrDescendant)!;

                var type = match.Groups["type"].Success
                    ? match.Groups["type"].Value.ToUpperInvariant() switch
                    {
                        "SS" => ProjectScheduleDependencyType.StartToStart,
                        "FF" => ProjectScheduleDependencyType.FinishToFinish,
                        "SF" => ProjectScheduleDependencyType.StartToFinish,
                        _ => ProjectScheduleDependencyType.FinishToStart
                    }
                    : ProjectScheduleDependencyType.FinishToStart;

                long lagMinutes = 0;
                if (match.Groups["lag"].Success)
                {
                    var lagText = match.Groups["lag"].Value
                        .Replace("d", "", StringComparison.OrdinalIgnoreCase).Replace(" ", "");
                    if (!double.TryParse(lagText, NumberStyles.Float, CultureInfo.InvariantCulture, out var lagDays))
                        return Result.Failure<ProjectScheduleTask>(ProjectErrors.InvalidPredecessorFormat)!;
                    lagMinutes = (long)Math.Round(lagDays * minutesPerDay);
                }

                parsed.Add(new ParsedPredecessor(pred, type, lagMinutes));
            }

            var liveIds = all.Select(t => t.Id).ToList();
            var allDependencies = (await _dependencyRepository.GetByTaskIds(liveIds, ct) ?? [])
                .Where(d => !d.IsDeleted).ToList();

            foreach (var p in parsed)
                if (CreatesCycle(p.Task.Id, task.Id, allDependencies))
                    return Result.Failure<ProjectScheduleTask>(ProjectErrors.PredecessorCycle)!;

            var existing = await _dependencyRepository.GetBySuccessorTaskIds([task.Id], ct) ?? [];
            foreach (var item in existing)
                await _dependencyRepository.Remove(item);

            foreach (var p in parsed)
                await _dependencyRepository.Create(
                    new ProjectScheduleTaskDependency(p.Task, task, p.Type, p.LagMinutes), ct);

            return task;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return Result.Failure<ProjectScheduleTask>(SharedErrors.UnknownError)!;
        }
    }
    private static bool CreatesCycle(
        long newPredecessorId,
        long successorId,
        List<ProjectScheduleTaskDependency> existingDependencies)
    {
        if (newPredecessorId == successorId)
            return true;

        var predecessorsOf = existingDependencies
            .GroupBy(d => d.SuccessorTaskId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.PredecessorTaskId).ToList());

        var visited = new HashSet<long>();
        var stack = new Stack<long>();
        stack.Push(newPredecessorId);

        while (stack.Count > 0)
        {
            var current = stack.Pop();
            if (current == successorId)
                return true;

            if (!visited.Add(current))
                continue;

            if (predecessorsOf.TryGetValue(current, out var preds))
                foreach (var p in preds)
                    stack.Push(p);
        }

        return false;
    }

    private static string NormalizeDigits(string s)
    {
        var sb = new StringBuilder(s.Length);
        foreach (var ch in s)
            sb.Append(ch switch
            {
                >= '\u06F0' and <= '\u06F9' => (char)('0' + (ch - '\u06F0')),
                >= '\u0660' and <= '\u0669' => (char)('0' + (ch - '\u0660')),
                '\u066B' => '.',
                '\u060C' => ',',
                _ => ch
            });
        return sb.ToString();
    }
}