using Aspose.Tasks;
using Aspose.Tasks.Saving;
using Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Errors;

namespace Engineering.Api.Helpers.MppTools;

public class MppExporter
{
    public Result<byte[]> Export(GetExportMppFileResponse response)
    {
        try
        {
            if (response is null || response.Tasks.Count == 0)
                return Result.Failure<byte[]>(
                    ProjectErrors.MppFileEmptyTask)!;

            var model = MapToMppImportModel(response);

            var project = new Project();

            if (!string.IsNullOrWhiteSpace(model.ProjectName))
                project.Set(Prj.Name, model.ProjectName);

            var calendarMap =
                MppExporterHelpers.BuildCalendars(
                    project,
                    model.Calendars);

            var wbsMap =
                MppExporterHelpers.BuildWbsTasks(
                    project,
                    model.Tasks);

            var taskMap =
                MppExporterHelpers.BuildActivityTasks(
                    project,
                    model.Tasks,
                    wbsMap,
                    calendarMap);

            MppExporterHelpers.BuildDependencies(
                project,
                model.Dependencies,
                taskMap);

            MppExporterHelpers.BuildBaselines(
                project,
                model.Tasks,
                taskMap);

            using var stream = new MemoryStream();

            project.Save(
                stream,
                SaveFileFormat.Mpp);


            return Result.Success(stream.ToArray());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static MppImportModel MapToMppImportModel(
        GetExportMppFileResponse response)
    {
        return new MppImportModel
        {
            ProjectName = response.ProjectName,

            Tasks = response.Tasks
                .Select(x => new MppTaskModel
                {
                    Id = x.Id,
                    Uid = x.Uid,
                    Name = x.Name,
                    SortOrder = x.SortOrder,
                    OutlineLevel = x.OutlineLevel,
                    OutlineNumber = x.OutlineNumber,
                    ParentUid = x.ParentUid,
                    CalendarUid = x.CalendarUid,
                    IsSummary = x.IsSummary,
                    IsMilestone = x.IsMilestone,
                    IsCritical = x.IsCritical,
                    Start = x.Start,
                    Finish = x.Finish,
                    DurationMinutes = x.DurationMinutes,
                    PercentComplete = x.PercentComplete,
                    BaselineStart = x.BaselineStart,
                    BaselineFinish = x.BaselineFinish,
                    BaselineDurationMinutes = x.BaselineDurationMinutes,
                    ActualStart = x.ActualStart,
                    ActualFinish = x.ActualFinish,
                    ActualDurationMinutes = x.ActualDurationMinutes
                })
                .ToList(),

            Dependencies = response.Dependencies
                .Select(x => new MppDependencyModel
                {
                    PredecessorUid = x.PredecessorUid,
                    SuccessorUid = x.SuccessorUid,
                    Type = x.Type,
                    LagMinutes = x.LagMinutes
                })
                .ToList(),

            Calendars = response.Calendars
                .Select(x => new MppCalendarModel
                {
                    Uid = x.Uid,
                    Name = x.Name,
                    IsDefault = x.IsDefault,
                    MinutesPerDay = x.MinutesPerDay,

                    WorkingDays = x.WorkingDays
                        .Select(d => new MppCalendarWorkingDayModel
                        {
                            DayOfWeek = d.DayOfWeek,
                            IsWorking = d.IsWorking,

                            WorkingTimes = d.WorkingTimes
                                .Select(t => new MppCalendarWorkingTimeModel
                                {
                                    From = t.From,
                                    To = t.To
                                })
                                .ToList()
                        })
                        .ToList(),

                    Exceptions = x.Exceptions
                        .Select(e => new MppCalendarExceptionModel
                        {
                            Date = e.Date,
                            IsWorking = e.IsWorking,
                            From = e.From,
                            To = e.To,
                            Description = e.Description
                        })
                        .ToList()
                })
                .ToList()
        };
    }
}