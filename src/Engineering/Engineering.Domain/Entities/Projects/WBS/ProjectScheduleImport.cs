using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleImport)]
public class ProjectScheduleImport : ActivateEntity<ProjectScheduleImport, long>
{
    [Description(WbsCmts.FileId)]
    public Guid? FileId { get; private set; }

    [Description(WbsCmts.FileName)]
    public string? FileName { get; private set; } = string.Empty;

    [Description(WbsCmts.ScheduleStartDate)]
    public DateTime? ScheduleStartDate { get; private set; }

    [Description(WbsCmts.ProjectScheduleImportStatus)]
    public ProjectScheduleImportStatus Status { get; private set; }

    [Description(WbsCmts.ImportedAt)]
    public DateTime ImportedAt { get; private set; }

    [Description(WbsCmts.ImportedBy)]
    public long ImportedBy { get; private set; }

    [Description(WbsCmts.ErrorMessage)]
    public string? ErrorMessage { get; private set; }

    [Description(WbsCmts.StatusDate)]
    public DateTime? StatusDate { get; private set; }

    [Description(WbsCmts.RescheduleFromDate)]
    public DateTime? RescheduleFromDate { get; private set; }

    [Description(WbsCmts.ProjectScheduleImport)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    public static ProjectScheduleImport FromMppFile(
        Project project, Guid fileId, string fileName, long importedBy)
    {
        var import = new ProjectScheduleImport
        {
            Project = project,
            ProjectId = project.Id,
            FileId = fileId,
            FileName = fileName,
            ImportedBy = importedBy,
            ImportedAt = DateTime.UtcNow,
            Status = ProjectScheduleImportStatus.Processing
        };
        import.SetActive();
        import.EnsureSystemColumns();
        return import;
    }

    public static ProjectScheduleImport CreateManual(
        Project project, DateTime scheduleStartDate, long createdBy)
    {
        var import = new ProjectScheduleImport
        {
            Project = project,
            ProjectId = project.Id,
            ScheduleStartDate = scheduleStartDate,
            ImportedBy = createdBy,
            ImportedAt = DateTime.UtcNow,
            Status = ProjectScheduleImportStatus.Completed
        };
        import.SetActive();
        import.EnsureSystemColumns();
        return import;
    }

    public void SetScheduleStartDate(DateTime value)
    {
        ScheduleStartDate = value;
    }

    public void SetCompleted()
    {
        Status = ProjectScheduleImportStatus.Completed;
        ErrorMessage = null;
    }

    public void SetArchived()
    {
        Status = ProjectScheduleImportStatus.Archived;
        ErrorMessage = null;
    }

    public void SetFileImportDetails(Guid fileId, string fileName)
    {
        FileId = fileId;
        FileName = fileName;
        Status = ProjectScheduleImportStatus.Processing;
        ErrorMessage = null;
    }

    public void SetStatusDate(DateTime? value)
    {
        StatusDate = value;
    }

    public void SetFailed(string errorMessage)
    {
        Status = ProjectScheduleImportStatus.Failed;
        ErrorMessage = errorMessage;
    }

    public void SetRescheduleFromDate(DateTime? value)
    {
        RescheduleFromDate = value;
    }

    public ProjectScheduleColumn AddCustomColumn(
        string titleFa, string? titleEn, ProjectScheduleColumnDataType dataType)
    {
        var sortOrder = _projectScheduleColumns.Count == 0
            ? 1
            : _projectScheduleColumns.Max(c => c.SortOrder) + 1;

        var column = new ProjectScheduleColumn(
            this, ProjectScheduleColumnType.Custom, titleFa, titleEn, sortOrder, dataType);

        _projectScheduleColumns.Add(column);
        return column;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<ProjectWbs> _projectWbses;
    public IReadOnlyList<ProjectWbs> ProjectWbses => _projectWbses;

    private readonly List<ProjectScheduleTask> _projectScheduleTasks;
    public IReadOnlyList<ProjectScheduleTask> ProjectScheduleTasks => _projectScheduleTasks;

    private readonly List<ProjectScheduleColumn> _projectScheduleColumns;
    public IReadOnlyList<ProjectScheduleColumn> ProjectScheduleColumns => _projectScheduleColumns;

    private ProjectScheduleImport()
    {
        _projectWbses = [];
        _projectScheduleTasks = [];
        _projectScheduleColumns = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    
    private static readonly (ProjectScheduleColumnType Type, string TitleFa, string TitleEn, ProjectScheduleColumnDataType DataType)[] SystemColumnDefaults =
    [
    (ProjectScheduleColumnType.Wbs, "ساختار شکست", "WBS", ProjectScheduleColumnDataType.String),
        (ProjectScheduleColumnType.Title, "نام فعالیت", "Activity Name", ProjectScheduleColumnDataType.String),
        (ProjectScheduleColumnType.Duration, "مدت زمان", "Duration", ProjectScheduleColumnDataType.Decimal),
        (ProjectScheduleColumnType.Start, "شروع", "Start", ProjectScheduleColumnDataType.DateTime),
        (ProjectScheduleColumnType.Finish, "پایان", "Finish", ProjectScheduleColumnDataType.DateTime),
        (ProjectScheduleColumnType.Predecessor, "پیش‌نیاز", "Predecessor", ProjectScheduleColumnDataType.String),
        (ProjectScheduleColumnType.Weight, "وزن", "Weight", ProjectScheduleColumnDataType.Decimal),
        (ProjectScheduleColumnType.PlannedProgress, "درصد پیشرفت برنامه‌ای", "Planned Progress", ProjectScheduleColumnDataType.Decimal),
        (ProjectScheduleColumnType.ActualProgress, "درصد پیشرفت واقعی", "Actual Progress", ProjectScheduleColumnDataType.Decimal),
    ];

    public void EnsureSystemColumns()
    {
        for (var i = 0; i < SystemColumnDefaults.Length; i++)
        {
            var d = SystemColumnDefaults[i];
            if (_projectScheduleColumns.Any(c => c.ColumnType == d.Type)) continue;

            _projectScheduleColumns.Add(new ProjectScheduleColumn(
                this, d.Type, d.TitleFa, d.TitleEn, i + 1, d.DataType));
        }
    }
}