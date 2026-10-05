using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

/// <summary>
/// کارکرد موقت
/// </summary>
public class ProjectOperationDetailInspection : AuditableEntity<ProjectOperationDetailInspection>
{

    [Description(ProjectDetailCmts.Length)]
    public decimal Length { get; private set; } = 1;

    [Description(ProjectDetailCmts.Width)]
    public decimal Width { get; private set; } = 1;

    [Description(ProjectDetailCmts.Height)]
    public decimal Height { get; private set; } = 1;

    [Description(ProjectDetailCmts.Weight)]
    public decimal Weight { get; private set; } = 1;

    [Description(ProjectDetailCmts.Number)]
    public decimal Number { get; private set; } = 1;

    [Description(ProjectDetailCmts.InspectionDate)]
    public DateTime? InspectionDate { get; set; }

    [Description(ProjectDetailCmts.Description)]
    public string? Description { get; set; }

    [Description(ProjectDetailCmts.FinalAmount)]
    [NotMapped]
    public decimal FinalAmount => Length * Width * Height * Weight * Number;

    [Description(GlobalCmts.Project)]
    public long ProjectId { get; set; }
    public Project Project { get; set; }

    [Description(GlobalCmts.OperationInfo)]
    public long? OperationInfoId { get; set; }
    public OperationInfo? OperationInfo { get; set; }

    [Description(GlobalCmts.OperationLocation)]
    public long? OperationLocationId { get; set; }
    public OperationLocation? OperationLocation { get; set; }

    [Description(GlobalCmts.ProjectOperation)]
    public long? ProjectOperationId { get; set; }
    public ProjectOperation? ProjectOperation { get; set; }

    [Description(ProjectDetailCmts.ProjectOperationDetail)]
    public long? ProjectOperationDetailId { get; set; }
    public ProjectOperationDetail? ProjectOperationDetail { get; set; }


    public ProjectOperationDetailInspection(Project project,
        OperationInfo? operationInfo,
        OperationLocation? operationLocation,
        ProjectOperation? projectOperation,
        ProjectOperationDetail? projectOperationDetail,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        DateTime? inspectionDate,
        string? description) : this()
    {
        SetProject(project);
        SetOperationInfo(operationInfo);
        SetOperationLocation(operationLocation);
        SetProjectOperation(projectOperation);
        SetProjectOperationDetail(projectOperationDetail);
        SetLength(length);
        SetWidth(width);
        SetHeight(height);
        SetHeight(height);
        SetNumber(number);
        SetDescription(description);
        SetInspectionDate(inspectionDate);
    }
    public static ProjectOperationDetailInspection Create(Project project,
        OperationInfo? operationInfo,
        OperationLocation? operationLocation,
        ProjectOperation? projectOperation,
        ProjectOperationDetail? projectOperationDetail,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        DateTime? inspectionDate,
        string? description)
    {
        return new ProjectOperationDetailInspection(project,
            operationInfo,
            operationLocation,
            projectOperation,
            projectOperationDetail,
            length,
            width,
            height,
            weight,
            number,
            inspectionDate,
            description);
    }
    #region Set data
    public void SetProject(Project value)
    {
        Project = Guard.Against.Null(value, nameof(value));
        ProjectId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetProjectOperationDetail(ProjectOperationDetail? value)
    {
        ProjectOperationDetail = value;
        ProjectOperationDetailId = value?.Id;
    }
    public void SetOperationInfo(OperationInfo? value)
    {
        OperationInfo = value;
        OperationInfoId = value?.Id;
    }
    public void SetOperationLocation(OperationLocation? value)
    {
        OperationLocation = value;
        OperationLocationId = value?.Id;
    }
    public void SetProjectOperation(ProjectOperation? value)
    {
        ProjectOperation = value;
    }
    public void SetLength(decimal value)
    {
        Length = Guard.Against.Null(value, nameof(value));
    }
    public void SetWidth(decimal value)
    {
        Width = Guard.Against.Null(value, nameof(value));
    }
    public void SetHeight(decimal value)
    {
        Height = Guard.Against.Null(value, nameof(value));
    }
    public void SetWeight(decimal value)
    {
        Weight = Guard.Against.Null(value, nameof(value));
    }
    public void SetNumber(decimal value)
    {
        Number = Guard.Against.Null(value, nameof(value));
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetInspectionDate(DateTime? value)
    {
        InspectionDate = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    #endregion

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            if (_projectOperationDetailInspectionDocuments.Any())
                _projectOperationDetailInspectionDocuments.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _projectOperationDetailInspectionDocuments.Add(ProjectOperationDetailInspectionDocument.Create(url, this));
        }
        else
        {
            if (_projectOperationDetailInspectionDocuments.Any())
                _projectOperationDetailInspectionDocuments.ForEach(c => c.SetIsDeleted());
        }
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ProjectOperationDetailInspectionDocument> _projectOperationDetailInspectionDocuments;
    public IReadOnlyList<ProjectOperationDetailInspectionDocument> ProjectOperationDetailInspectionDocuments => _projectOperationDetailInspectionDocuments;
    private ProjectOperationDetailInspection()
    {
        _projectOperationDetailInspectionDocuments = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
