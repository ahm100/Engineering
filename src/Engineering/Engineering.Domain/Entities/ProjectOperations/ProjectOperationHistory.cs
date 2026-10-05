using Engineering.Domain.Entities.ProjectOperations.Enums;

namespace Engineering.Domain.Entities.ProjectOperations;

[Description(EContractCmts.ProjectOperationPriceHistory)]
public class ProjectOperationHistory : AuditableEntity<ProjectOperationHistory>
{
    [Description(ProjectOperationCmts.Price)]
    public decimal? UnitPrice { get; private set; }
    [Description(ProjectOperationCmts.TolerancePercentage)]
    public decimal TolerancePercentage { get; private set; }
    [Description(ProjectOperationCmts.Workload)]
    public decimal Workload { get; private set; }
    [Description(ProjectOperationCmts.Priority)]
    public int? Priority { get; private set; }
    [Description(ProjectOperationCmts.UnitOfMeasurementId)]
    public long UnitOfMeasurementId { get; private set; }
    [Description(ProjectOperationCmts.BasePrice)]
    public decimal BasePrice { get; private set; } = 0;
    [Description(ProjectOperationCmts.ChangedPrice)]
    public decimal ChangedPrice { get; private set; } = 0;
    [Description(ProjectOperationCmts.ProjectOperationStatus)]
    public ProjectOperationStatus ProjectOperationStatus { get; private set; } = ProjectOperationStatus.NotStarted;
    [Description(ProjectOperationCmts.GoodsInProgress)]
    public bool GoodsInProgress { get; private set; } = false;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }
    [Description(GlobalCmts.ProjectOperation)]
    public ProjectOperation ProjectOperation { get; private set; }
    public long ProjectOperationId { get; private set; }

    /// Timing

    [Description(ProjectOperationCmts.PlannedStartDate)]
    public DateTime? PlannedStartDate { get; private set; }

    [Description(ProjectOperationCmts.PlannedFinishDate)]
    public DateTime? PlannedFinishDate { get; private set; }

    [Description(ProjectOperationCmts.PlannedDuration)]
    public int? PlannedDuration { get; private set; }

    [Description(ProjectOperationCmts.ActualStartDate)]
    public DateTime? ActualStartDate { get; private set; }

    [Description(ProjectOperationCmts.ActualFinishDate)]
    public DateTime? ActualFinishDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineStartDate)]
    public DateTime? BaselineStartDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineFinishDate)]
    public DateTime? BaselineFinishDate { get; private set; }

    [Description(ProjectOperationCmts.BaselineDuration)]
    public int? BaselineDuration { get; private set; }

    public ProjectOperationHistory(
        ProjectOperation projectOperation) : this()
    {
        SetProjectOperation(projectOperation);
        SetWorkLoad(projectOperation.Workload);
        SetTolerancePercentage(projectOperation.TolerancePercentage);
        SetPrice(projectOperation.Price);
        SetPriority(projectOperation.Priority);
        SetUnitOfMeasurementId(projectOperation.UnitOfMeasurementId);
        SetProjectOperationStatus(projectOperation.ProjectOperationStatus);
        SetGoodsInProgress(projectOperation.GoodsInProgress);
        SetDescription(projectOperation.Description);
        SetPlannedStartDate(projectOperation.PlannedStartDate);
        SetPlannedFinishDate(projectOperation.PlannedFinishDate);
        SetPlannedDuration(projectOperation.PlannedDuration);
        SetActualStartDate(projectOperation.ActualStartDate);
        SetActualFinishDate(projectOperation.ActualFinishDate);
        SetBaselineStartDate(projectOperation.BaselineStartDate);
        SetBaselineFinishDate(projectOperation.BaselineFinishDate);
        SetBaselineDuration(projectOperation.BaselineDuration);
    }

    public ProjectOperationHistory Create(
        ProjectOperation projectOperation)
    {
        return new ProjectOperationHistory(
            projectOperation);
    }

    #region Set data

    public void SetBasePrice(decimal value)
    {
        BasePrice = Guard.Against.Null(value, nameof(value));
    }
    public void SetChangedPrice(decimal value)
    {
        ChangedPrice = Guard.Against.Null(value, nameof(value));
    }
    private void SetWorkLoad(decimal value)
    {
        Workload = Guard.Against.Null(value, nameof(value));
    }
    private void SetDescription(string? value)
    {
        Description = value;
    }
    private void SetProjectOperation(ProjectOperation value)
    {
        ProjectOperation = Guard.Against.Null(value, nameof(value));
        ProjectOperationId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    private void SetPrice(decimal? value)
    {
        UnitPrice = value;
    }
    private void SetTolerancePercentage(decimal value)
    {
        UnitPrice = Guard.Against.Null(value, nameof(value));
    }
    private void SetPriority(decimal? value)
    {
        UnitPrice = value;
    }
    private void SetUnitOfMeasurementId(decimal value)
    {
        UnitPrice = Guard.Against.Null(value, nameof(value));
    }
    private void SetProjectOperationStatus(ProjectOperationStatus value)
    {
        ProjectOperationStatus = Guard.Against.Null(value, nameof(value));
    }
    private void SetGoodsInProgress(bool value)
    {
        GoodsInProgress = Guard.Against.Null(value, nameof(value));
    }
    public void SetPlannedStartDate(DateTime? value)
    {
        PlannedStartDate = value;
    }
    public void SetPlannedFinishDate(DateTime? value)
    {
        PlannedFinishDate = value;
    }

    public void SetPlannedDuration(int? value)
    {
        PlannedDuration = value;
    }

    public void SetActualStartDate(DateTime? value)
    {
        ActualStartDate = value;
    }

    public void SetActualFinishDate(DateTime? value)
    {
        ActualFinishDate = value;
    }

    public void SetBaselineStartDate(DateTime? value)
    {
        BaselineStartDate = value;
    }

    public void SetBaselineFinishDate(DateTime? value)
    {
        BaselineFinishDate = value;
    }

    public void SetBaselineDuration(int? value)
    {
        BaselineDuration = value;
    }

    #endregion


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private ProjectOperationHistory() { }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
