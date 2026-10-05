using Engineering.Domain.Entities.Projects.Enums;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectScheduleColumn)]
public class ProjectScheduleColumn : ActivateEntity<ProjectScheduleColumn, long>
{
    [Description(WbsCmts.ProjectScheduleImport)]
    public long ProjectScheduleImportId { get; private set; }
    public ProjectScheduleImport ProjectScheduleImport { get; private set; }

    [Description(WbsCmts.ColumnType)]
    public ProjectScheduleColumnType ColumnType { get; private set; }

    [Description(GlobalCmts.TitleFa)]
    public string TitleFa { get; private set; } = string.Empty;

    [Description(GlobalCmts.TitleEn)]
    public string? TitleEn { get; private set; }

    [Description(WbsCmts.SortOrder)]
    public int SortOrder { get; private set; }

    [Description(WbsCmts.DataType)]
    public ProjectScheduleColumnDataType DataType { get; private set; }

    public ProjectScheduleColumn(
        ProjectScheduleImport import,
        ProjectScheduleColumnType columnType,
        string titleFa,
        string? titleEn,
        int sortOrder,
        ProjectScheduleColumnDataType dataType = ProjectScheduleColumnDataType.String) : this()
    {
        ProjectScheduleImport = Guard.Against.Null(import);

        ColumnType = columnType;
        TitleFa = Guard.Against.NullOrWhiteSpace(titleFa).Trim();
        SetTitleEn(titleEn);
        SortOrder = sortOrder;
        DataType = dataType;

        SetActive();
    }

    public void SetTitleFa(string value)
    {
        TitleFa = Guard.Against.NullOrWhiteSpace(value).Trim();
    }

    public void SetTitleEn(string? value)
    {
        TitleEn = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public void SetSortOrder(int value)
    {
        SortOrder = value;
    }

    public void SetDataType(ProjectScheduleColumnDataType value)
    {
        DataType = value;
    }

    private readonly List<ProjectScheduleTaskValue> _projectScheduleTaskValues;
    public IReadOnlyList<ProjectScheduleTaskValue> ProjectScheduleTaskValues => _projectScheduleTaskValues;

#pragma warning disable CS8618
    private ProjectScheduleColumn()
    {
        _projectScheduleTaskValues = [];
    }
#pragma warning restore CS8618
}