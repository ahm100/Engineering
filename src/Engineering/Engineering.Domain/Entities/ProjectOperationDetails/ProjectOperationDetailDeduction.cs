using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

public class ProjectOperationDetailDeduction : AuditableEntity<ProjectOperationDetailDeduction>
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

    [Description(ProjectDetailCmts.FinalAmount)]
    [NotMapped]
    public decimal FinalAmount => Length * Width * Height * Weight * Number;

    [Description(GlobalCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    public ProjectOperationDetailDeduction(
        ProjectOperationDetail projectOperationDetail,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number) : this()
    {
        SetProjectOperationDetail(projectOperationDetail);
        SetLength(length);
        SetWidth(width);
        SetHeight(height);
        SetWeight(weight);
        SetNumber(number);
    }

    public static ProjectOperationDetailDeduction Create(
        ProjectOperationDetail projectOperationDetail,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number)
    {
        return new ProjectOperationDetailDeduction(
            projectOperationDetail,
            length,
            width,
            height,
            weight,
            number);
    }

    #region Set data

    public void SetProjectOperationDetail(ProjectOperationDetail projectOperationDetail)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ProjectOperationDetailId = Guard.Against.Null(projectOperationDetail.Id, nameof(projectOperationDetail.Id));
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
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDetailDeduction()
    {
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
