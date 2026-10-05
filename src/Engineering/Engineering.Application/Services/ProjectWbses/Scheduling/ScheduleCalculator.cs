using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.WBS;

namespace Engineering.Application.Services.ProjectWbses.Scheduling;

public static class ScheduleCalculator
{
    /// <param name="rescheduleFrom">
    /// When set, incomplete work is pushed to start on/after this date ("reschedule uncompleted work").
    /// Null = plain recalculation; the status date is NOT used here anymore.
    /// </param>
    public static void Recalculate(
        IReadOnlyCollection<ProjectScheduleTask> allTasks,
        IReadOnlyCollection<ProjectScheduleTaskDependency> allDeps,
        WorkCalendar cal,
        DateTime projectStart,
        DateTime? rescheduleFrom = null)
    {
        // soft-deleted rows never take part in scheduling
        var tasks = allTasks.Where(t => !t.IsDeleted).ToList();
        var byId = tasks.ToDictionary(t => t.Id);
        var deps = allDeps.Where(d => !d.IsDeleted
                && byId.ContainsKey(d.PredecessorTaskId)
                && byId.ContainsKey(d.SuccessorTaskId)).ToList();

        var childrenOf = tasks.ToLookup(t => t.ParentId);
        var predsOf = deps.ToLookup(d => d.SuccessorTaskId);
        var succsOf = deps.ToLookup(d => d.PredecessorTaskId);
        var done = new HashSet<long>();
        var visiting = new HashSet<long>();
        var baseStart = cal.SnapForward(projectStart);

        static decimal ProgressOf(ProjectScheduleTask t) => t.PercentComplete;

        // Complete (locked) if ANY of these is true. Independent of any date.
        static bool IsComplete(ProjectScheduleTask t) =>
            t.PercentComplete >= 100
            || t.PhysicalPercentComplete >= 100
            || t.ActualFinish.HasValue;

        static DateTime Max(DateTime a, DateTime b) => a > b ? a : b;
        static DateTime Min(DateTime a, DateTime b) => a < b ? a : b;

        static decimal Effective(ProjectScheduleTask t, Func<ProjectScheduleTask, decimal> pick)
            => IsComplete(t) ? 100m : pick(t);

        // duration-weighted average, same idea as MS Project summary % complete
        static decimal WeightedAverage(
            IReadOnlyCollection<ProjectScheduleTask> items, Func<ProjectScheduleTask, decimal> pick)
        {
            var weights = items.Sum(c => (decimal)(c.PlannedDurationMinutes ?? 0));
            if (weights <= 0)
                return Math.Round(items.Average(c => Effective(c, pick)), 2);
            return Math.Round(
                items.Sum(c => (c.PlannedDurationMinutes ?? 0) * Effective(c, pick)) / weights, 2);
        }

        static void RollUpProgress(ProjectScheduleTask t, List<ProjectScheduleTask> children)
        {
            t.SetPercentComplete(WeightedAverage(children, c => c.PercentComplete));
            t.SetPhysicalPercentComplete(WeightedAverage(children, c => c.PhysicalPercentComplete));
            t.SetActualDurationMinutes(children.Sum(c => c.ActualDurationMinutes ?? 0));

            var starts = children.Where(c => c.ActualStart.HasValue).Select(c => c.ActualStart!.Value).ToList();
            t.SetActualStart(starts.Count > 0 ? starts.Min() : null);

            // SetPercentComplete(<100) already cleared ActualFinish; set it only when everything is done
            if (children.All(IsComplete))
                t.SetActualFinish(children.Max(c => c.ActualFinish ?? c.PlannedFinish!.Value));
            else
                t.SetActualFinish(null);
        }

        void Compute(ProjectScheduleTask t)
        {
            if (done.Contains(t.Id)) return;
            if (!visiting.Add(t.Id)) throw new InvalidOperationException("Dependency cycle detected.");

            var children = childrenOf[t.Id].ToList();

            if (children.Count > 0)
            {
                // Summary: everything derived from children.
                children.ForEach(Compute);
                var s = children.Min(c => c.PlannedStart!.Value);
                var f = children.Max(c => c.PlannedFinish!.Value);
                if (f < s) f = s;
                t.SetIsSummary(true);
                t.SetSchedule(s, f, cal.WorkingMinutesBetween(s, f), children.Any(c => c.IsEstimated));
                t.SetRemainingDuration(children.Sum(c => c.RemainingDurationMinutes ?? 0));
                RollUpProgress(t, children);
            }
            else
            {
                // A parent that lost its last child is a normal task again.
                if (t.IsSummary == true) t.SetIsSummary(false);

                var isComplete = IsComplete(t);
                var progress = ProgressOf(t);
                var started = !t.IsMilestone && (progress > 0 || t.ActualStart.HasValue);

                // Stored duration is always respected; the 1-day default is only for tasks with no duration.
                var duration = t.IsMilestone ? 0L : t.PlannedDurationMinutes ?? cal.MinutesPerDay;

                var remaining = isComplete ? 0L
                    : started ? (long)Math.Round(duration * (1 - progress / 100m))
                    : duration;

                var actualDuration = t.IsMilestone ? 0L
                    : isComplete ? duration
                    : (long)Math.Round(duration * progress / 100m);

                var pinned = t.IsManuallyScheduled && t.PlannedStart.HasValue;
                var hasDates = t.PlannedStart.HasValue && t.PlannedFinish.HasValue;
                var hasActualDates = t.ActualStart.HasValue && t.ActualFinish.HasValue;

                if (isComplete && (hasDates || hasActualDates))
                {
                    if (!hasDates)
                        t.SetSchedule(t.ActualStart!.Value, t.ActualFinish!.Value,
                            t.PlannedDurationMinutes ?? duration, false);
                    else if (t.PlannedFinish < t.PlannedStart)
                        t.SetSchedule(t.PlannedStart!.Value,
                            cal.AddWorkingMinutes(cal.SnapForward(t.PlannedStart.Value), duration),
                            duration, t.IsEstimated);

                    t.SetRemainingDuration(0);
                    if (t.ActualDurationMinutes is null)
                        t.SetActualDurationMinutes(t.PlannedDurationMinutes ?? duration);
                }
                else if (pinned && t.PlannedFinish.HasValue)
                {
                    t.SetRemainingDuration(remaining);
                    t.SetActualDurationMinutes(actualDuration);
                }
                else
                {
                    // 1) constraints from predecessors
                    var start = pinned ? t.PlannedStart!.Value : baseStart;
                    DateTime? finishBound = null;

                    if (!pinned)
                    {
                        foreach (var d in predsOf[t.Id])
                        {
                            var p = byId[d.PredecessorTaskId];
                            Compute(p);

                            switch (d.Type)
                            {
                                case ProjectScheduleDependencyType.FinishToStart:
                                    start = Max(start, cal.Shift(p.PlannedFinish!.Value, d.LagMinutes)); break;
                                case ProjectScheduleDependencyType.StartToStart:
                                    start = Max(start, cal.Shift(p.PlannedStart!.Value, d.LagMinutes)); break;
                                case ProjectScheduleDependencyType.FinishToFinish:
                                    finishBound = finishBound is { } a
                                        ? Max(a, cal.Shift(p.PlannedFinish!.Value, d.LagMinutes))
                                        : cal.Shift(p.PlannedFinish!.Value, d.LagMinutes); break;
                                case ProjectScheduleDependencyType.StartToFinish:
                                    finishBound = finishBound is { } b
                                        ? Max(b, cal.Shift(p.PlannedStart!.Value, d.LagMinutes))
                                        : cal.Shift(p.PlannedStart!.Value, d.LagMinutes); break;
                            }
                        }
                    }

                    // 2) which work is being scheduled now
                    var rescheduling = rescheduleFrom.HasValue && !isComplete;
                    var len = rescheduling ? remaining : duration;

                    var earliest = start;
                    if (rescheduling) earliest = Max(earliest, rescheduleFrom!.Value);
                    if (finishBound is { } fb) earliest = Max(earliest, cal.SubtractWorkingMinutes(fb, len));

                    var resume = cal.SnapForward(earliest);
                    var finish = t.IsMilestone ? resume : cal.AddWorkingMinutes(resume, len);

                    // 3) the done part stays where it happened, only the rest moves
                    var taskStart = rescheduling && started
                        ? Min(t.ActualStart ?? cal.SnapForward(start), resume)
                        : resume;

                    t.SetSchedule(taskStart, finish, duration, !t.IsMilestone && t.IsEstimated);
                    t.SetRemainingDuration(remaining);
                    t.SetActualDurationMinutes(actualDuration);
                }
            }

            visiting.Remove(t.Id);
            done.Add(t.Id);
        }

        void MarkCritical()
        {
            var leaves = tasks.Where(t => !childrenOf[t.Id].Any()).ToList();
            if (leaves.Count == 0) return;
            var projectFinish = leaves.Max(l => l.PlannedFinish!.Value);

            var lateStart = new Dictionary<long, DateTime>();
            var lateFinish = new Dictionary<long, DateTime>();
            var inProgress = new HashSet<long>();
            var flagged = new Dictionary<long, bool>();

            DateTime Back(DateTime dt, long lag)
                => lag >= 0 ? cal.SubtractWorkingMinutes(dt, lag) : cal.AddWorkingMinutes(dt, -lag);

            void Late(ProjectScheduleTask t)
            {
                if (lateFinish.ContainsKey(t.Id)) return;
                if (!inProgress.Add(t.Id)) throw new InvalidOperationException("Dependency cycle detected.");

                var kids = childrenOf[t.Id].ToList();
                if (kids.Count > 0)
                {
                    kids.ForEach(Late);
                    lateStart[t.Id] = kids.Min(k => lateStart[k.Id]);
                    lateFinish[t.Id] = kids.Max(k => lateFinish[k.Id]);
                }
                else
                {
                    var dur = t.IsMilestone ? 0L
                        : cal.WorkingMinutesBetween(t.PlannedStart!.Value, t.PlannedFinish!.Value);
                    var lf = projectFinish;

                    foreach (var d in succsOf[t.Id])
                    {
                        var s = byId[d.SuccessorTaskId];
                        if (IsComplete(s)) continue; // finished work no longer constrains anything
                        Late(s);

                        var limit = d.Type switch
                        {
                            ProjectScheduleDependencyType.FinishToStart =>
                                Back(lateStart[s.Id], d.LagMinutes),
                            ProjectScheduleDependencyType.FinishToFinish =>
                                Back(lateFinish[s.Id], d.LagMinutes),
                            ProjectScheduleDependencyType.StartToStart =>
                                cal.AddWorkingMinutes(Back(lateStart[s.Id], d.LagMinutes), dur),
                            ProjectScheduleDependencyType.StartToFinish =>
                                cal.AddWorkingMinutes(Back(lateFinish[s.Id], d.LagMinutes), dur),
                            _ => projectFinish
                        };
                        if (limit < lf) lf = limit;
                    }

                    lateFinish[t.Id] = lf;
                    lateStart[t.Id] = dur == 0 ? lf : cal.SubtractWorkingMinutes(lf, dur);
                }

                inProgress.Remove(t.Id);
            }

            bool Flag(ProjectScheduleTask t)
            {
                if (flagged.TryGetValue(t.Id, out var known)) return known;

                var kids = childrenOf[t.Id].ToList();
                var critical = false;
                if (kids.Count > 0)
                {
                    foreach (var k in kids) critical |= Flag(k); // no short-circuit: every child gets flagged
                }
                else if (!IsComplete(t))
                {
                    var ef = t.PlannedFinish!.Value;
                    var lf = lateFinish[t.Id];
                    var slack = lf <= ef ? 0 : cal.WorkingMinutesBetween(ef, lf);
                    critical = slack == 0;
                }

                t.SetIsCritical(critical);
                flagged[t.Id] = critical;
                return critical;
            }

            foreach (var t in tasks) Late(t);
            foreach (var t in tasks) Flag(t);
        }

        foreach (var t in tasks) Compute(t);
        MarkCritical();
    }
}