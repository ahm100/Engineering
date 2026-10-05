using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.Projects.WBS;

[Description(WbsCmts.ProjectWbs)]
public class ProjectWbs : ActivateEntity<ProjectWbs, long>
{
    [Description(GlobalCmts.Title)]
    public string TitleFa { get; private set; } = string.Empty;

    [Description(GlobalCmts.Title)]
    public string? TitleEn { get; private set; } = string.Empty;

    [Description(GlobalCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(GlobalCmts.Description)]
    public string? DescriptionFa { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; }

    [Description(WbsCmts.MppUid)]
    public int? MppUid { get; private set; }

    [Description(WbsCmts.OutlineLevel)]
    public int? OutlineLevel { get; private set; }

    [Description(WbsCmts.OutlineNumber)]
    public string? OutlineNumber { get; private set; }

    [Description(WbsCmts.SortOrder)]
    public int SortOrder { get; private set; }

    [Description(WbsCmts.ProjectScheduleImportId)]
    public long? ProjectScheduleImportId { get; private set; }
    public ProjectScheduleImport? ProjectScheduleImport { get; private set; }

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; private set; }
    public Project Project { get; private set; }

    [Description(WbsCmts.ProjectWbs)]
    public long? ParentId { get; private set; }
    public ProjectWbs? Parent { get; private set; }

    public ProjectWbs(
        Project project,
        ProjectWbs? parent,
        ProjectScheduleImport? projectScheduleImport,
        string titleFa,
        string? titleEn,
        string code,
        string? descriptionFa,
        string? descriptionEn,
        bool isActive) : this()
    {
        SetProject(project);
        SetParent(parent);
        SetProjectScheduleImport(projectScheduleImport);

        SetTitleFa(titleFa);
        SetTitleEn(titleEn);

        SetCode(code);

        SetDescriptionFa(descriptionFa);
        SetDescriptionEn(descriptionEn);

        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    public void Update(
        string titleFa,
        string? titleEn,
        string code,
        string? descriptionFa,
        string? descriptionEn,
        bool? isActive)
    {
        SetTitleFa(titleFa);
        SetTitleEn(titleEn);

        SetCode(code);

        SetDescriptionFa(descriptionFa);
        SetDescriptionEn(descriptionEn);

        if (isActive is not null && isActive.Value)
            SetActive();
        else if (isActive is not null && !isActive.Value)
            SetDeactivate();
    }

    public void SetTitleFa(string value)
    {
        TitleFa = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetTitleEn(string? value)
    {
        TitleEn = value;
    }

    public void SetCode(string value)
    {
        Code = Guard.Against.NullOrEmpty(value, nameof(value));
    }

    public void SetParent(ProjectWbs? value)
    {
        Parent = value;
        ParentId = value?.Id;
    }

    public void SetProjectScheduleImport(ProjectScheduleImport? value)
    {
        ProjectScheduleImport = value;
        ProjectScheduleImportId = value?.Id;
    }

    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetDescriptionFa(string? value)
    {
        DescriptionFa = value;
    }

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }

    public void SetProjectOperationWbs(List<ProjectOperation> values)
    {
        foreach (var item in values)
            _projectOperationWbses.Add(new ProjectOperationWbs(item, this, true));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<ProjectWbs> _childs;
    public IReadOnlyList<ProjectWbs> Childs => _childs;

    private readonly List<ProjectOperationWbs> _projectOperationWbses;
    public IReadOnlyList<ProjectOperationWbs> ProjectOperationWbses => _projectOperationWbses;

    private readonly List<ProjectScheduleTask> _projectScheduleTasks;
    public IReadOnlyList<ProjectScheduleTask> ProjectScheduleTasks => _projectScheduleTasks;

    private ProjectWbs()
    {
        _childs = [];
        _projectOperationWbses = [];
        _projectScheduleTasks = [];
    }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}