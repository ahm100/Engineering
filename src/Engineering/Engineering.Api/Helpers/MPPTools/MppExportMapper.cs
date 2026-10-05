using Engineering.Api.Helpers.MPPTools;
using Engineering.Application.Services.ProjectWbses.Contracts.GetExportMppFile;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Api.Helpers.MppTools;

public static class MppExportMapper
{
    public static MppImportModel ToMppImportModel(
        this GetExportMppFileResponse response)
    {
        var customColumns = new List<MppCustomColumnModel>();
        var codeByColumnId = new Dictionary<long, string>();
        var usedPerType = new Dictionary<ProjectScheduleColumnDataType, int>();

        foreach (var c in response.CustomColumns.OrderBy(c => c.SortOrder))
        {
            usedPerType.TryGetValue(c.DataType, out var index);
            usedPerType[c.DataType] = index + 1;

            var (code, fieldId) = MppCustomFieldSlots.Slot(c.DataType, index);
            customColumns.Add(new MppCustomColumnModel
            {
                FieldId = fieldId,
                Code = code,
                Alias = c.Title,
                DataType = c.DataType
            });
            codeByColumnId[c.Id] = code;
        }

        var model = new MppImportModel
        {
            ProjectName = response.ProjectName,
            StartDate = response.StartDate,
            StatusDate = response.StatusDate,
            CustomColumns = customColumns,
            Tasks = response.Tasks.Select(x => new MppTaskModel
            {
                IsManual = x.IsManuallyScheduled,
                IsEstimated = x.IsEstimated,
                PhysicalPercentComplete = x.PhysicalPercentComplete,
                RemainingDurationMinutes = x.RemainingDurationMinutes,
                Deadline = x.Deadline,
                Cost = x.Cost,
                Note = x.Note,
                CustomValues = x.CustomValues
                    .Where(v => codeByColumnId.ContainsKey(v.ColumnId))
                    .Select(v => new MppCustomValueModel
                    {
                        ColumnCode = codeByColumnId[v.ColumnId],
                        StringValue = v.StringValue,
                        DecimalValue = v.DecimalValue,
                        DateTimeValue = v.DateTimeValue
                    }).ToList()
            }).ToList(),

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
                .ToList(),

            // Enable UI styling
            UiConfig = new UiStylingConfig()
        };

        return model;
    }
}