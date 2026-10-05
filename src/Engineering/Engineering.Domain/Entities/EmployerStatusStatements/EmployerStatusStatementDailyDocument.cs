
namespace Engineering.Domain.Entities.EmployerStatusStatements;
/// <summary>
/// مستندات پیوست قرارداد پیمانکار
/// </summary>
public class EmployerStatusStatementDailyDocument : AuditableEntity<EmployerStatusStatementDailyDocument>
{
    /// <summary>
    /// لینک
    /// </summary>
    public string Url { get; private set; } = string.Empty;

    /// <summary>
    /// قرارداد پیمانکار
    /// </summary>
    public EmployerStatusStatementProjectOperationDetailDaily EmployerStatusStatementProjectOperationDetailDaily { get; private set; }

    public EmployerStatusStatementDailyDocument(string url, EmployerStatusStatementProjectOperationDetailDaily employerStatusStatementProjectOperationDetailDaily)
    {
        Url = Guard.Against.NullOrEmpty(url, nameof(url));
        EmployerStatusStatementProjectOperationDetailDaily = Guard.Against.Null(employerStatusStatementProjectOperationDetailDaily, nameof(employerStatusStatementProjectOperationDetailDaily));
    }

    public static EmployerStatusStatementDailyDocument Create(string url, EmployerStatusStatementProjectOperationDetailDaily employerStatusStatementProjectOperationDetailDaily)
    {
        return new EmployerStatusStatementDailyDocument(url, employerStatusStatementProjectOperationDetailDaily);
    }

    public void SetIsDeleted()
    {
        this.IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private EmployerStatusStatementDailyDocument() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
