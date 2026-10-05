using Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;
using Engineering.Application.Services.ProjectWbses.ImportMPP;

namespace Engineering.Api.Helpers.MPPTools;

public static class MppExportMapping
{
    public static MppImportModel ToMppImportModel(this GetExportMppFileResponse r) => new()
    {
        ProjectName = r.ProjectName,
        StartDate = r.StartDate,
        Tasks = r.Tasks.Select(t => new MppTaskModel
        {
            Id = t.Id,
            Uid = t.Uid,
            Name = t.Name,
            ParentUid = t.ParentUid,
            SortOrder = t.SortOrder,
            OutlineLevel = t.OutlineLevel,
            OutlineNumber = t.OutlineNumber,
            IsSummary = t.IsSummary,
            IsMilestone = t.IsMilestone,
            IsCritical = t.IsCritical,
            IsManual = t.IsManuallyScheduled,
            Start = t.Start,
            Finish = t.Finish,
            DurationMinutes = t.DurationMinutes,
            PercentComplete = t.PercentComplete,
            PhysicalPercentComplete = t.PhysicalPercentComplete,
            RemainingDurationMinutes = t.RemainingDurationMinutes,
            BaselineStart = t.BaselineStart,
            BaselineFinish = t.BaselineFinish,
            BaselineDurationMinutes = t.BaselineDurationMinutes,
            ActualStart = t.ActualStart,
            ActualFinish = t.ActualFinish,
            ActualDurationMinutes = t.ActualDurationMinutes,
            Deadline = t.Deadline,
            Cost = t.Cost,
            Note = t.Note,
            IsEstimated = t.IsEstimated
        }).ToList(),
        Dependencies = r.Dependencies.Select(d => new MppDependencyModel
        {
            PredecessorUid = d.PredecessorUid,
            SuccessorUid = d.SuccessorUid,
            Type = d.Type,
            LagMinutes = d.LagMinutes
        }).ToList(),
        Calendars = r.Calendars.Select(c => new MppCalendarModel
        {
            Uid = c.Uid,
            Name = c.Name,
            IsDefault = c.IsDefault,
            MinutesPerDay = c.MinutesPerDay,
            WorkingDays = c.WorkingDays.Select(d => new MppCalendarWorkingDayModel
            {
                DayOfWeek = d.DayOfWeek,
                IsWorking = d.IsWorking,
                WorkingTimes = d.WorkingTimes.Select(t => new MppCalendarWorkingTimeModel
                { From = t.From, To = t.To }).ToList()
            }).ToList(),
            Exceptions = c.Exceptions.Select(e => new MppCalendarExceptionModel
            { Date = e.Date, IsWorking = e.IsWorking, From = e.From, To = e.To, Description = e.Description }).ToList()
        }).ToList()
    };
}