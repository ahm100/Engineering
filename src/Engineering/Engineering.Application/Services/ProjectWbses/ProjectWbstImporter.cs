using Engineering.Application.Abstractions.Data.Projects;
using Engineering.Application.Abstractions.Data.Projects.ProjectCalendars;
using Engineering.Application.Abstractions.Data.Projects.WBS;
using Engineering.Application.Services.ProjectWbses.ImportMPP;
using Engineering.Domain.Entities.Projects.Enums;
using Engineering.Domain.Entities.Projects.ProjectCalendars;
using Engineering.Domain.Entities.Projects.WBS;
using ProjectEntity = Engineering.Domain.Entities.Projects.Project;


namespace Engineering.Application.Services.ProjectWbses;

public class ProjectWbstImporter
{
    private const int TitleMax = 250, NoteMax = 1500;

    private readonly ILogger<ProjectWbstImporter> _logger;
    private readonly IMppParser _parser;
    private readonly IProjectRepository _projectRepository;
    private readonly IProjectScheduleImportRepository _importRepository;
    private readonly IProjectScheduleTaskRepository _taskRepository;
    private readonly IProjectScheduleTaskDependencyRepository _dependencyRepository;
    private readonly IProjectCalendarRepository _projectCalendarRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProjectScheduleTaskValueRepository _valueRepository;

    public ProjectWbstImporter(
        ILogger<ProjectWbstImporter> logger,
        IMppParser parser,
        IProjectRepository projectRepository,
        IProjectScheduleImportRepository importRepository,
        IProjectScheduleTaskRepository taskRepository,
        IProjectScheduleTaskDependencyRepository dependencyRepository,
        IProjectCalendarRepository projectCalendarRepository,
        IUnitOfWork unitOfWork,
        IProjectScheduleTaskValueRepository valueRepository)
    {
        _logger = logger;
        _parser = parser;
        _projectRepository = projectRepository;
        _importRepository = importRepository;
        _taskRepository = taskRepository;
        _dependencyRepository = dependencyRepository;
        _projectCalendarRepository = projectCalendarRepository;
        _unitOfWork = unitOfWork;
        _valueRepository = valueRepository;
    }

    public async Task<Result<long>> ImportMppFileManager(
        long projectId,
        Guid fileId,
        string fileName,
        long importedBy,
        Stream file, CT ct)
    {
        var project = await _projectRepository.GetById(projectId, ct);
        if (project is null)
            return Result.Failure<long>(ProjectErrors.ProjectNotFound);

        if (await _importRepository.GetByProjectId(projectId, ct) is not null)
            return Result.Failure<long>(ProjectErrors.ProjectImportAlreadyExist);

        var parse = await _parser.ParseAsync(file, ct);
        if (parse.IsFailure || parse.Value is null)
            return Result.Failure<long>(GlobalErrors.ErrorOnReadFile);
        var model = parse.Value;

        var validation = MppImportModelValidator.Validate(model);
        if (!validation.IsSuccess)
            return Result.Failure<long>(validation.Error ?? GlobalErrors.InValidRequest);

        await _unitOfWork.BeginTransactionAsync(ct);
        try
        {
            var import = ProjectScheduleImport.FromMppFile(project, fileId, fileName, importedBy);
            import.SetScheduleStartDate(MppImportModelValidator.ResolveStartDate(model)!.Value);
            import.SetStatusDate(model.StatusDate);

            var customColumns = AddCustomColumns(import, model.CustomColumns);
            await _importRepository.Create(import, ct);

            var calendars = await ImportCalendarsAsync(project, model.Calendars, ct);
            var taskMap = await ImportTasksAsync(import, model.Tasks, calendars, customColumns, ct);

            await _importRepository.Create(import, ct);
            await ImportDependenciesAsync(model.Dependencies, taskMap, ct);

            import.SetCompleted();
            await _unitOfWork.CommitAsync(ct);
            await _unitOfWork.CommitTransactionAsync(ct);
            return import.Id;
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync(ct);
            if (ex is OperationCanceledException) throw;
            _logger.LogError(ex, "MPP import failed, ProjectId:{ProjectId}", projectId);
            return Result.Failure<long>(ProjectErrors.MppImportFailed);
        }
    }

    private async Task<(Dictionary<int, ProjectCalendar> ByUid, int DefaultUid)> ImportCalendarsAsync(
        ProjectEntity project,
        IReadOnlyCollection<MppCalendarModel> calendars, CT ct)
    {
        var defaultUid = (calendars.FirstOrDefault(c => c.IsDefault) ?? calendars.First()).Uid;
        var existing = await _projectCalendarRepository.GetByProjectId(project.Id, ct) ?? [];
        foreach (var old in existing)
            old.SetDefault(false);

        var byUid = new Dictionary<int, ProjectCalendar>();
        foreach (var m in calendars)
        {
            var cal = new ProjectCalendar(project, Truncate(m.Name, TitleMax)!, Truncate(m.Name, TitleMax),
                m.Uid == defaultUid, m.MinutesPerDay ?? 480);
            cal.SetMppUid(m.Uid);

            foreach (var d in m.WorkingDays)
                cal.SetWorkingDay(d.DayOfWeek, d.IsWorking,
                    d.IsWorking ? d.WorkingTimes.Select(t => (t.From, t.To)).ToList() : null);

            foreach (var e in m.Exceptions.GroupBy(x => x.Date.Date).Select(g => g.Last()))
                cal.AddException(e.Date, e.IsWorking, Truncate(e.Description, NoteMax), e.From, e.To);

            await _projectCalendarRepository.Create(cal, ct);
            byUid.TryAdd(m.Uid, cal);
        }
        return (byUid, defaultUid);
    }

    private async Task<Dictionary<int, ProjectScheduleTask>> ImportTasksAsync(
        ProjectScheduleImport import,
        IReadOnlyCollection<MppTaskModel> tasks,
        (Dictionary<int, ProjectCalendar> ByUid, int DefaultUid) calendars,
        IReadOnlyDictionary<string, ProjectScheduleColumn> customColumns,
        CT ct)
    {
        var map = new Dictionary<int, ProjectScheduleTask>();
        var layout = BuildLayout(tasks, out var ordered);

        foreach (var m in ordered)
        {
            var uid = m.Uid!.Value;
            var (sortOrder, level, number) = layout[uid];

            ProjectScheduleTask? parent = null;
            if (m.ParentUid.HasValue) map.TryGetValue(m.ParentUid.Value, out parent);

            var title = string.IsNullOrWhiteSpace(m.Name) ? $"Task {m.Id ?? uid}" : m.Name.Trim();

            var task = new ProjectScheduleTask(
                import, projectWbs: null, title: Truncate(title, TitleMax)!,
                mppUid: uid, mppId: m.Id,
                sortOrder: sortOrder,
                outlineLevel: level,
                outlineNumber: number,
                plannedStart: m.Start,
                plannedFinish: m.Finish,
                plannedDurationMinutes: m.DurationMinutes,
                percentComplete: m.PercentComplete ?? 0,
                baselineStart: m.BaselineStart,
                baselineFinish: m.BaselineFinish,
                baselineDurationMinutes: m.BaselineDurationMinutes,
                actualStart: m.ActualStart,
                actualFinish: m.ActualFinish,
                actualDurationMinutes: m.ActualDurationMinutes,
                isMilestone: m.IsMilestone,
                isCritical: m.IsCritical,
                isSummary: m.IsSummary,
                isManuallyScheduled: m.IsManual);

            task.SetParentTask(parent);

            if (m.CalendarUid is { } calUid && calUid != calendars.DefaultUid && 
                    calendars.ByUid.TryGetValue(calUid, out var taskCalendar))
                task.SetCalendar(taskCalendar);

            task.SetPhysicalPercentComplete(m.PhysicalPercentComplete ?? 0);
            task.SetRemainingDuration(m.RemainingDurationMinutes);
            task.SetEstimated(m.IsEstimated);
            task.SetDeadline(m.Deadline);
            task.SetCost(m.Cost);
            task.SetNote(Truncate(m.Note, NoteMax));

            await _taskRepository.Create(task, ct);
            map.Add(uid, task);
        }
        return map;
    }

    private async Task ImportDependenciesAsync(
        IReadOnlyCollection<MppDependencyModel> dependencies,
        IReadOnlyDictionary<int, ProjectScheduleTask> map, CT ct)
    {
        var seen = new HashSet<(int, int)>();
        foreach (var d in dependencies)
        {
            if (d.PredecessorUid is not { } p || d.SuccessorUid is not { } s || p == s) continue;
            if (!map.TryGetValue(p, out var pred) || !map.TryGetValue(s, out var succ)) continue;
            if (!seen.Add((p, s))) continue;

            await _dependencyRepository.Create(
                new ProjectScheduleTaskDependency(pred, succ, MapDependencyType(d.Type), d.LagMinutes), ct);
        }
    }

    private static string? Truncate(string? v, int max) =>
        v is null || v.Length <= max ? v : v[..max];

    private static ProjectScheduleDependencyType MapDependencyType(
        MppDependencyType type)
    {
        return type switch
        {
            MppDependencyType.FinishToStart => ProjectScheduleDependencyType.FinishToStart,
            MppDependencyType.StartToStart => ProjectScheduleDependencyType.StartToStart,
            MppDependencyType.FinishToFinish => ProjectScheduleDependencyType.FinishToFinish,
            MppDependencyType.StartToFinish => ProjectScheduleDependencyType.StartToFinish,
            _ => throw new ArgumentOutOfRangeException(
                nameof(type),
                type,
                "نوع وابستگی پشتیبانی نمی‌شود.")
        };
    }

    private static Dictionary<int, (int SortOrder, int Level, string Number)> BuildLayout(
        IReadOnlyCollection<MppTaskModel> tasks,
        out List<MppTaskModel> ordered)
    {
        var known = tasks.Select(t => t.Uid!.Value).ToHashSet();
        int? ParentOf(MppTaskModel t) => t.ParentUid is { } p && known.Contains(p) ? p : null;

        var byParent = tasks.ToLookup(ParentOf);
        var layout = new Dictionary<int, (int SortOrder, int Level, string Number)>();
        var list = new List<MppTaskModel>(tasks.Count);

        void Walk(int? parentUid, int level, string prefix)
        {
            var i = 0;
            foreach (var t in byParent[parentUid].OrderBy(x => x.SortOrder))   // x.SortOrder = MPP row id
            {
                i++;
                var number = prefix.Length == 0 ? i.ToString() : $"{prefix}.{i}";
                layout[t.Uid!.Value] = (i, level, number);
                list.Add(t);
                Walk(t.Uid, level + 1, number);
            }
        }

        Walk(null, 1, "");
        ordered = list;
        return layout;
    }

    private static Dictionary<string, ProjectScheduleColumn> AddCustomColumns(
        ProjectScheduleImport import,
        IReadOnlyCollection<MppCustomColumnModel> columns)
    {
        var byCode = new Dictionary<string, ProjectScheduleColumn>(StringComparer.OrdinalIgnoreCase);
        var usedTitles = import.ProjectScheduleColumns
            .Select(c => c.TitleFa)
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var c in columns)
        {
            if (byCode.ContainsKey(c.Code)) continue;

            var baseTitle = Truncate(string.IsNullOrWhiteSpace(c.Alias) ? c.Code : c.Alias.Trim(), TitleMax)!;
            var title = baseTitle;
            var n = 1;
            // titles are unique per schedule (system titles included), so disambiguate collisions
            while (!usedTitles.Add(title))
                title = Truncate(n++ == 1 ? $"{baseTitle} ({c.Code})" : $"{baseTitle} ({c.Code}) {n}", TitleMax)!;

            byCode[c.Code] = import.AddCustomColumn(title, null, c.DataType);
        }
        return byCode;
    }

    private async Task ImportCustomValuesAsync(
        ProjectScheduleTask task,
        IReadOnlyCollection<MppCustomValueModel> values,
        IReadOnlyDictionary<string, ProjectScheduleColumn> columns, CT ct)
    {
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var v in values)
        {
            if (!v.HasValue || !columns.TryGetValue(v.ColumnCode, out var column)) continue;
            if (!seen.Add(v.ColumnCode)) continue;   // unique index: one value per task and column

            var cell = new ProjectScheduleTaskValue(task, column);
            switch (column.DataType)
            {
                case ProjectScheduleColumnDataType.Decimal:
                    if (v.DecimalValue is null) continue;
                    cell.SetNumber(v.DecimalValue);
                    break;
                case ProjectScheduleColumnDataType.DateTime:
                    if (v.DateTimeValue is null) continue;
                    cell.SetDateTime(v.DateTimeValue);
                    break;
                default:
                    if (string.IsNullOrWhiteSpace(v.StringValue)) continue;
                    cell.SetText(Truncate(v.StringValue.Trim(), NoteMax));
                    break;
            }
            await _valueRepository.Create(cell, ct);
        }
    }
}