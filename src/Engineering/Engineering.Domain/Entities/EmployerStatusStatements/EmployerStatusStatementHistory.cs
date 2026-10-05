using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Domain.Entities.EmployerStatusStatements;

/// <summary>
/// صورت وضعیت کارفرما
/// </summary>
public class EmployerStatusStatementHistory : AuditableEntity<EmployerStatusStatementHistory>
{
    /// <summary>
    /// کد صورت وضعیت
    /// </summary>
    public string? StatusStatementCode { get; private set; }
    /// <summary>
    /// وضعیت ارسالی
    /// </summary>
    public EmployerStatusStatementStatus Status { get; private set; }
    /// <summary>
    /// تاریخ شروع
    /// </summary>
    public DateTime StartDate { get; private set; }
    /// <summary>
    /// تاریخ پایان
    /// </summary>
    public DateTime EndDate { get; private set; }
    /// <summary>
    /// درصد کار انجام شده
    /// </summary>
    public decimal PercentageOfWorkDone { get; private set; }
    /// <summary>
    /// مبلغ محاسبه شده
    /// </summary>
    public decimal CalculatedAmount { get; private set; }
    /// <summary>
    /// حجم صورت وضعیت
    /// </summary>
    public decimal StatusStatementVolume { get; private set; }
    /// <summary>
    /// توضیحات
    /// </summary>
    public string? Description { get; private set; } = string.Empty;

    public EmployerStatusStatement EmployerStatusStatement { get; private set; }

    public EmployerStatusStatementHistory(
        EmployerStatusStatement employerStatusStatement,
        EmployerStatusStatementStatus employerStatusStatementStatus,
        DateTime startDate,
        DateTime endDate,
        decimal percentageOfWorkDone,
        decimal calculatedAmount,
        decimal statusStatementVolume,
        string? statusStatementCode,
        string? description)
    {
        EmployerStatusStatement = employerStatusStatement;
        StartDate = Guard.Against.Null(startDate, nameof(startDate));
        EndDate = Guard.Against.Null(endDate, nameof(endDate));
        PercentageOfWorkDone = Guard.Against.Null(percentageOfWorkDone, nameof(percentageOfWorkDone));
        StatusStatementVolume = Guard.Against.Null(statusStatementVolume, nameof(statusStatementVolume));
        CalculatedAmount = Guard.Against.Null(calculatedAmount, nameof(calculatedAmount));
        Description = description;
        StatusStatementCode = statusStatementCode;
        Status = employerStatusStatementStatus;

    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementHistory()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
