using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Domain.Entities.ProjectOperationDetails;

public class ProjectOperationDetailHistory : AuditableEntity<ProjectOperationDetailHistory>
{
    [Description(ProjectDetailCmts.Code)]
    public string Code { get; private set; } = string.Empty;

    [Description(ProjectDetailCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(ProjectDetailCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

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

    [Description(ProjectDetailCmts.Priority)]
    public int Priority { get; private set; }

    [Description(ProjectDetailCmts.Day)]
    public int Day { get; private set; }

    [Description(ProjectDetailCmts.Hour)]
    public int Hour { get; private set; }

    [Description(ProjectDetailCmts.FinalAmount)]
    public decimal FinalAmount { get; private set; }

    [Description(ProjectDetailCmts.Status)]
    public ProjectOperationDetailStatus? Status { get; private set; } = ProjectOperationDetailStatus.NotStarted;

    [Description(ProjectDetailCmts.Description)]
    public string? Description { get; private set; } = string.Empty;

    [Description(ProjectDetailCmts.StatusDescription)]
    public string? StatusDescription { get; private set; } = string.Empty;

    [Description(ProjectDetailCmts.ProjectOperationDetail)]
    public long ProjectOperationDetailId { get; private set; }
    public ProjectOperationDetail ProjectOperationDetail { get; private set; }

    public ProjectOperationDetailHistory(
        ProjectOperationDetail projectOperationDetail,
        string code,
        DateTime? startDate,
        DateTime? endDate,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        decimal finalAmount,
        ProjectOperationDetailStatus? status,
        int priority,
        int day,
        int hour,
        string? description,
        string? statusDescription) : this()
    {
        SetProjectOperationDetail(projectOperationDetail);
        SetCode(code);
        SetPriority(priority);
        SetDay(day);
        SetHour(hour);
        SetLength(length);
        SetWidth(width);
        SetHeight(height);
        SetWeight(weight);
        SetNumber(number);
        SetStatus(status);
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetStatusDescription(statusDescription);
        SetFinalAmount(finalAmount);
    }

    public static ProjectOperationDetailHistory Create(
        ProjectOperationDetail projectOperationDetail,
        string code,
        DateTime? startDate,
        DateTime? endDate,
        decimal length,
        decimal width,
        decimal height,
        decimal weight,
        decimal number,
        decimal finalAmount,
        ProjectOperationDetailStatus? status,
        int priority,
        int day,
        int hour,
        string? description,
        string? statusDescription)
    {
        return new ProjectOperationDetailHistory(
            projectOperationDetail,
            code,
            startDate,
            endDate,
            length,
            width,
            height,
            weight,
            number,
            finalAmount,
            status,
            priority,
            day,
            hour,
            description,
            statusDescription);
    }

    #region Set data

    public void SetProjectOperationDetail(ProjectOperationDetail projectOperationDetail)
    {
        ProjectOperationDetail = Guard.Against.Null(projectOperationDetail, nameof(projectOperationDetail));
        ProjectOperationDetailId = Guard.Against.Null(projectOperationDetail.Id, nameof(projectOperationDetail.Id));
    }

    public void SetCode(string code)
    {
        Code = Guard.Against.NullOrWhiteSpace(code, nameof(code));
    }

    public void SetPriority(int priority)
    {
        Priority = Guard.Against.Null(priority, nameof(priority));
    }

    public void SetDay(int day)
    {
        Day = Guard.Against.Null(day, nameof(day));
    }

    public void SetHour(int hour)
    {
        Hour = Guard.Against.Null(hour, nameof(hour));
    }

    public void SetLength(decimal length)
    {
        Length = Guard.Against.Null(length, nameof(length));
    }

    public void SetWidth(decimal width)
    {
        Width = Guard.Against.Null(width, nameof(width));
    }

    public void SetHeight(decimal height)
    {
        Height = Guard.Against.Null(height, nameof(height));
    }

    public void SetWeight(decimal weight)
    {
        Weight = Guard.Against.Null(weight, nameof(weight));
    }

    public void SetNumber(decimal number)
    {
        Number = Guard.Against.Null(number, nameof(number));
    }

    public void SetFinalAmount(decimal finalAmount)
    {
        FinalAmount = Guard.Against.Null(finalAmount, nameof(finalAmount));
    }

    public void SetStatus(ProjectOperationDetailStatus? status)
    {
        Status = status;
    }

    public void SetStartDate(DateTime? startDate)
    {
        StartDate = startDate;
    }

    public void SetEndDate(DateTime? endDate)
    {
        EndDate = endDate;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public void SetStatusDescription(string? value)
    {
        StatusDescription = value;
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ProjectOperationDetailHistory()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
