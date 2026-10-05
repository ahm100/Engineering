namespace Engineering.Application.Services.ProjectWbses.ImportMPP;

public static class MppImportModelValidator
{
    public static DateTime? ResolveStartDate(MppImportModel m) =>
        m.StartDate ?? m.Tasks.Where(t => t.Start.HasValue).Min(t => t.Start);

    public static Result Validate(MppImportModel? model)
    {
        if (model is null) return Result.Failure(GlobalErrors.ErrorOnReadFile);
        if (model.Tasks.Count == 0) return Result.Failure(ProjectErrors.MppFileEmptyTask);

        if (model.Tasks.Any(t => t.Uid is null) ||
            model.Tasks.GroupBy(t => t.Uid).Any(g => g.Count() > 1))
            return Result.Failure(ProjectErrors.MppFileDuplicateTasks);

        var uids = model.Tasks.Select(t => t.Uid!.Value).ToHashSet();
        if (model.Tasks.Any(t => t.ParentUid.HasValue && !uids.Contains(t.ParentUid.Value)))
            return Result.Failure(ProjectErrors.MppFileInvalidHierarchy);

        if (ResolveStartDate(model) is null)
            return Result.Failure(ProjectErrors.MppFileNoStartDate);

        if (model.Calendars.GroupBy(c => c.Uid).Any(g => g.Count() > 1))
            return Result.Failure(ProjectErrors.MppFileDuplicateCalanders);

        foreach (var cal in model.Calendars)
        {
            if (!cal.WorkingDays.Any(d => d.IsWorking))
                return Result.Failure(ProjectErrors.CalendarWithoutWorkingTime);

            foreach (var day in cal.WorkingDays)
            {
                if (!day.IsWorking && day.WorkingTimes.Count > 0)
                    return Result.Failure(ProjectErrors.CannotDefineWorkingTimeForClosedDays);
                if (day.IsWorking && day.WorkingTimes.Count == 0)
                    return Result.Failure(ProjectErrors.WorkingTimeInvalid);

                var ordered = day.WorkingTimes.OrderBy(t => t.From).ToList();
                if (ordered.Any(t => t.From >= t.To))
                    return Result.Failure(ProjectErrors.WorkingTimeInvalid);
                for (var i = 1; i < ordered.Count; i++)
                    if (ordered[i].From < ordered[i - 1].To)
                        return Result.Failure(ProjectErrors.WorkingTimeInvalid);
            }
        }
        return Result.Success();
    }
}