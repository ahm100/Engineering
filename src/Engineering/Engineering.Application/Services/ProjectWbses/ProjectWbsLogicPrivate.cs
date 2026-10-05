using Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;
using Engineering.Application.Services.ProjectWbses.Contracts.GetProjectSchedule;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses;

public partial class ProjectWbsLogic
{
    private static List<GetExportMppTaskModel> BuildExportTasks(
        IReadOnlyCollection<ProjectScheduleTask> tasks,
        IReadOnlyDictionary<long, int> uidById,
        IReadOnlyDictionary<long, int> calendarUidById,
        IReadOnlyDictionary<long, List<GetExportMppCustomValueModel>> valuesByTaskId)
    {
        var parentIds = tasks.Where(t => t.ParentId.HasValue)
            .Select(t => t.ParentId!.Value).ToHashSet();

        return tasks
            .OrderBy(t => t.SortOrder)
            .Select((t, index) => new GetExportMppTaskModel
            {
                Id = index + 1,   // row number follows the export order
                Uid = uidById[t.Id],
                Name = t.Title,
                ParentUid = t.ParentId is { } p && uidById.TryGetValue(p, out var puid) ? puid : null,
                CalendarUid = t.CalendarId is { } calId && calendarUidById.TryGetValue(calId, out var calUid) ? calUid : null,
                SortOrder = t.SortOrder,
                OutlineLevel = t.OutlineLevel,
                OutlineNumber = t.OutlineNumber,
                IsSummary = parentIds.Contains(t.Id),
                Start = t.PlannedStart,
                Finish = t.PlannedFinish,
                DurationMinutes = t.PlannedDurationMinutes,
                PercentComplete = t.PercentComplete,
                BaselineStart = t.BaselineStart,
                BaselineFinish = t.BaselineFinish,
                BaselineDurationMinutes = t.BaselineDurationMinutes,
                ActualStart = t.ActualStart,
                ActualFinish = t.ActualFinish,
                ActualDurationMinutes = t.ActualDurationMinutes,
                IsMilestone = t.IsMilestone,
                PhysicalPercentComplete = t.PhysicalPercentComplete,
                RemainingDurationMinutes = t.RemainingDurationMinutes,
                Deadline = t.Deadline,
                Cost = t.Cost,
                Note = t.Note,
                IsEstimated = t.IsEstimated,
                IsCritical = t.IsCritical,
                CustomValues = valuesByTaskId.TryGetValue(t.Id, out var cv) ? cv : [],
                IsManuallyScheduled = t.IsManuallyScheduled
            })
            .ToList();
    }

    private async Task<Result<T>> RunAndRecalculateAsync<T>(
        long projectId, Func<Task<Result<T>>> command, CT ct, bool recalculate = true)
    {
        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            var result = await command();
            if (result.IsFailure)
            {
                await _unitOfWork.RollbackTransactionAsync(ct);
                return result;
            }

            await _unitOfWork.CommitAsync(ct);

            if (recalculate)
            {
                var recalc = await _scheduleRecalculator.RecalculateProjectAsync(projectId, ct);
                if (recalc.IsFailure)
                {
                    await _unitOfWork.RollbackTransactionAsync(ct);
                    return Result.Failure<T>(recalc.Error!)!;
                }
                await _unitOfWork.CommitAsync(ct);
            }
            await _unitOfWork.CommitTransactionAsync(ct);
            return result;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(ct);
            throw;
        }
    }

    private async Task<long?> GetProjectIdByCalendarAsync(long calendarId, CT ct)
        => (await _projectCalendarRepository.GetById(calendarId, ct))?.ProjectId;

    private async Task<long?> GetProjectIdByTaskAsync(long taskId, CT ct)
    {
        var task = await _taskRepository.GetById(taskId, ct);
        if (task?.ProjectScheduleImportId is not { } importId) return null;
        return (await _importRepository.GetById(importId, ct))?.ProjectId;
    }

    private static List<GetExportMppDependencyModel> BuildExportDependencies(
        IReadOnlyCollection<ProjectScheduleTaskDependency> dependencies,
        IReadOnlyDictionary<long, int> uidById)
    {
        var result = new List<GetExportMppDependencyModel>();

        foreach (var d in dependencies)
        {
            if (!uidById.TryGetValue(d.PredecessorTaskId, out var predUid)) continue;
            if (!uidById.TryGetValue(d.SuccessorTaskId, out var succUid)) continue;

            result.Add(new GetExportMppDependencyModel
            {
                PredecessorUid = predUid,
                SuccessorUid = succUid,
                Type = MapDependencyTypeToMpp(d.Type),
                LagMinutes = d.LagMinutes
            });
        }
        return result;
    }

    private static Dictionary<long, int> BuildTaskUidMap(
        IReadOnlyCollection<ProjectScheduleTask> tasks)
    {
        var used = tasks.Where(t => t.MppUid.HasValue).Select(t => t.MppUid!.Value).ToHashSet();
        var next = used.Count == 0 ? 1 : used.Max() + 1;

        var map = new Dictionary<long, int>();
        foreach (var t in tasks.OrderBy(t => t.SortOrder))
            map[t.Id] = t.MppUid ?? next++;
        return map;
    }


    private static MppDependencyType MapDependencyTypeToMpp(
        ProjectScheduleDependencyType type)
    {
        return type switch
        {
            ProjectScheduleDependencyType.FinishToStart =>
                MppDependencyType.FinishToStart,

            ProjectScheduleDependencyType.StartToStart =>
                MppDependencyType.StartToStart,

            ProjectScheduleDependencyType.FinishToFinish =>
                MppDependencyType.FinishToFinish,

            ProjectScheduleDependencyType.StartToFinish =>
                MppDependencyType.StartToFinish,

            _ => throw new ArgumentOutOfRangeException(
                nameof(type),
                type,
                "نوع وابستگی نامعتبر است.")
        };
    }

    private static List<GetExportMppCalendarModel> BuildExportCalendars(
        IReadOnlyCollection<ProjectCalendar>? calendars,
        IReadOnlyDictionary<long, int> calendarUidById)
    {
        if (calendars is null)
            return [];

        var ordered = calendars.OrderByDescending(x => x.IsDefault).ThenBy(x => x.TitleFa).ToList();
        var used = ordered.Where(c => c.MppUid.HasValue).Select(c => c.MppUid!.Value).ToHashSet();
        var next = used.Count == 0 ? 1 : used.Max() + 1;

        var result = new List<GetExportMppCalendarModel>();
        foreach (var x in ordered)
        {
            result.Add(new GetExportMppCalendarModel
            {

                Uid = calendarUidById[x.Id],
                Name = x.TitleFa,
                IsDefault = x.IsDefault,
                MinutesPerDay = x.MinutesPerDay,

                WorkingDays = x.ProjectCalendarWorkingDaies
                    .OrderBy(x => x.DayOfWeek)
                    .Select(d => new GetExportMppWorkingDayModel
                    {
                        DayOfWeek = d.DayOfWeek,
                        IsWorking = d.IsWorking,

                        WorkingTimes = d.ProjectCalendarWorkingTimes
                            .OrderBy(x => x.From)
                            .Select(t => new GetExportMppWorkingTimeModel
                            {
                                From = t.From,
                                To = t.To
                            })
                            .ToList()
                    })
                    .ToList(),

                Exceptions = x.ProjectCalendarExceptions
                    .OrderBy(x => x.Date)
                    .Select(e => new GetExportMppCalendarExceptionModel
                    {
                        Date = e.Date,
                        IsWorking = e.IsWorking,
                        From = e.From,
                        To = e.To,
                        Description = e.Description
                    })
                    .ToList()
            });
        }
        return result;
    }


    /// <summary>
    /// یک تسک، بر اساس تاریخ موردنظر، طبق برنامه چند درصد باید پیش رفته باشد.
    /// خروجی فقط بر اساس تاریخ‌های برنامه‌ای (Planned) محاسبه می‌شود، نه پیشرفت واقعی.
    /// </summary>
    private static decimal CalculateTaskSchedulePercent(
        DateTime? start, DateTime? finish, DateTime asOf, WorkCalendar? cal, bool isMilestone = false)
    {
        if (start is null || finish is null) return 0;
        if (isMilestone) return asOf >= start ? 100 : 0;
        if (finish <= start) return 0;
        if (asOf <= start) return 0;
        if (asOf >= finish) return 100;

        if (cal is not null)
        {
            var total = cal.WorkingMinutesBetween(start.Value, finish.Value);
            if (total > 0)
            {
                var elapsed = cal.WorkingMinutesBetween(start.Value, asOf);
                return Math.Round((decimal)elapsed / total * 100, 2);
            }
        }
        return Math.Round((decimal)((asOf - start.Value).TotalMinutes / (finish.Value - start.Value).TotalMinutes) * 100, 2);
    }

    /// <summary>
    /// میانگین وزنی (بر اساس مدت زمان برنامه‌ای) یک مجموعه تسک، برای هر متریک پیشرفتی که به آن پاس داده شود.
    /// هم برای % Plan و هم % Actual استفاده می‌شود — فقط selector فرق می‌کند.
    /// این همان محلی است که فرمول محاسباتی تعریف می‌شود؛ در آینده در صورت نیاز به فرمول‌های دیگر
    /// (مثلاً میانگین ساده، یا وزن‌دهی بر اساس هزینه) فقط همین متد جایگزین می‌شود.
    /// </summary>
    private static decimal CalculateDurationWeightedAverage(
        IReadOnlyCollection<ProjectScheduleTask> tasks,
        Func<ProjectScheduleTask, decimal> percentSelector)
    {
        decimal weightedSum = 0;
        decimal totalWeight = 0;

        foreach (var task in tasks)
        {
            var weight = task.PlannedDurationMinutes is > 0
                ? (decimal)task.PlannedDurationMinutes.Value
                : 0m; // milestones (0 duration) don't skew the average

            weightedSum += weight * percentSelector(task);
            totalWeight += weight;
        }

        return totalWeight > 0 ? Math.Round(weightedSum / totalWeight, 2) : 0m;
    }

    /// <summary>
    /// % Plan کل پروژه — میانگین وزنی «% Complete برنامه‌ای» همه تسک‌ها، بر اساس تاریخ موردنظر.
    /// </summary>
    private static decimal CalculateProjectPlanPercent(
        IReadOnlyCollection<ProjectScheduleTask> tasks,
        DateTime asOfDate,
        WorkCalendar? cal)
    {
        return CalculateDurationWeightedAverage(
            tasks,
            t => CalculateTaskSchedulePercent(t.PlannedStart, t.PlannedFinish, asOfDate, cal));
    }

    /// <summary>
    /// % Actual کل پروژه — میانگین وزنی پیشرفت فیزیکی واقعی (PhysicalPercentComplete) همه تسک‌ها.
    /// </summary>
    private static decimal CalculateProjectActualPercent(
        IReadOnlyCollection<ProjectScheduleTask> tasks)
    {
        return CalculateDurationWeightedAverage(
            tasks,
            t => t.PhysicalPercentComplete);
    }

    private static Dictionary<long, (decimal PlanPercent, decimal ActualPercent)> CalculateTaskRollups(
        IReadOnlyCollection<ProjectScheduleTask> tasks,
        DateTime asOfDate,
        WorkCalendar? cal)
    {
        var result = new Dictionary<long, (decimal, decimal)>();
        var childrenByParent = tasks.ToLookup(t => t.ParentId);

        List<ProjectScheduleTask> LeafDescendants(long taskId)
        {
            var leaves = new List<ProjectScheduleTask>();
            foreach (var child in childrenByParent[taskId])
            {
                if (childrenByParent[child.Id].Any())
                    leaves.AddRange(LeafDescendants(child.Id));
                else
                    leaves.Add(child);
            }
            return leaves;
        }

        void Visit(ProjectScheduleTask task)
        {
            var hasChildren = childrenByParent[task.Id].Any();
            if (hasChildren)
            {
                var leaves = LeafDescendants(task.Id);
                result[task.Id] = (
                    CalculateProjectPlanPercent(leaves, asOfDate, cal),
                    CalculateProjectActualPercent(leaves));

                foreach (var child in childrenByParent[task.Id])
                    Visit(child);
            }
        }

        foreach (var root in childrenByParent[null])
            Visit(root);

        return result;
    }

    private static List<GetProjectScheduleTaskModel> BuildTaskTree(
        IReadOnlyCollection<ProjectScheduleTask> tasks,
        Dictionary<long, int> childCounts,
        Dictionary<long, (decimal PlanPercent, decimal ActualPercent)> rollups,
        DateTime asOfDate,
        WorkCalendar? cal)
    {
        var nodeById = new Dictionary<long, GetProjectScheduleTaskModel>();

        foreach (var x in tasks)
        {
            var childCount = childCounts.GetValueOrDefault(x.Id);
              var schedulePct = CalculateTaskSchedulePercent(x.PlannedStart, x.PlannedFinish, asOfDate, cal, x.IsMilestone);
            var (plan, actual) = rollups.TryGetValue(x.Id, out var r) ? r : (schedulePct, x.PhysicalPercentComplete);

            nodeById[x.Id] = new GetProjectScheduleTaskModel
            {
                Id = x.Id,
                ParentTaskId = x.ParentId,
                Title = x.Title,
                MppId = x.MppId,
                MppUid = x.MppUid,
                HasChildren = childCount > 0,
                CountChildren = childCount,
                IsSummary = childCount > 0,
                IsEstimated = x.IsEstimated,
                SortOrder = x.SortOrder,
                OutlineLevel = x.OutlineLevel,
                OutlineNumber = x.OutlineNumber,
                PlannedStart = x.PlannedStart,
                PlannedFinish = x.PlannedFinish,
                PlannedDurationMinutes = x.PlannedDurationMinutes,
                PercentComplete = x.PercentComplete,
                PhysicalPercentComplete = x.PhysicalPercentComplete,
                SchedulePercentComplete = schedulePct,
                PlanPercent = plan,
                ActualPercent = actual,
                BaselineStart = x.BaselineStart,
                BaselineFinish = x.BaselineFinish,
                BaselineDurationMinutes = x.BaselineDurationMinutes,
                ActualStart = x.ActualStart,
                ActualFinish = x.ActualFinish,
                ActualDurationMinutes = x.ActualDurationMinutes,
                IsMilestone = x.IsMilestone,
                IsCritical = x.IsCritical,
                IsManuallyScheduled = x.IsManuallyScheduled,
                Children = []
            };
        }

        var roots = new List<GetProjectScheduleTaskModel>();
        foreach (var x in tasks)
        {
            var node = nodeById[x.Id];

            if (x.ParentId.HasValue && nodeById.TryGetValue(x.ParentId.Value, out var parent))
            {
                parent.Children.Add(node);
            }
            else
            {
                roots.Add(node);
            }
        }

        void SortRecursive(List<GetProjectScheduleTaskModel> nodes)
        {
            nodes.Sort((a, b) => a.SortOrder.CompareTo(b.SortOrder));
            foreach (var n in nodes) SortRecursive(n.Children);
        }
        SortRecursive(roots);

        return roots;
    }
    private static Dictionary<long, int> BuildCalendarUidMap(
        IReadOnlyCollection<ProjectCalendar>? calendars)
    {
        var map = new Dictionary<long, int>();
        if (calendars is null) return map;

        var ordered = calendars.OrderByDescending(x => x.IsDefault).ThenBy(x => x.TitleFa).ToList();
        var used = new HashSet<int>();
        foreach (var c in ordered)
            if (c.MppUid is { } u && used.Add(u)) map[c.Id] = u;

        var next = used.Count == 0 ? 1 : used.Max() + 1;
        foreach (var c in ordered)
            if (!map.ContainsKey(c.Id)) map[c.Id] = next++;
        return map;
    }

    private static GetProjectScheduleColumnModel ToColumnModel(ProjectScheduleColumn c) => new()
    {
        Id = c.Id,
        Type = c.ColumnType,
        TitleFa = c.TitleFa,
        TitleEn = c.TitleEn,
        SortOrder = c.SortOrder,
        DataType = c.DataType,
        IsSystem = c.ColumnType != ProjectScheduleColumnType.Custom
    };

    private static void ApplyColumnData(
        IEnumerable<GetProjectScheduleTaskModel> nodes,
        Dictionary<long, List<GetProjectScheduleTaskValueModel>> valuesByTask,
        Dictionary<long, decimal> weightByTask)
    {
        foreach (var node in nodes)
        {
            if (weightByTask.TryGetValue(node.Id, out var weight))
                node.Weight = weight;

            if (valuesByTask.TryGetValue(node.Id, out var customValues))
                node.CustomValues = customValues;

            if (node.Children.Count > 0)
                ApplyColumnData(node.Children, valuesByTask, weightByTask);
        }
    }
}
